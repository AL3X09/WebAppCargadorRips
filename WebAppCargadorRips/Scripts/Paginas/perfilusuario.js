/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806
//window.onload = function(){
$(document).ready(function () {
  
  jsGrid.locale("es");

  /**
  * FIN FUNCIONES ANONIMAS
  */
  getAllME();



});

function getAllME() {
    var token = $('#codigospan').text();

    $.ajax({
        type: "GET",
        url: baseURL + "api/Usuarios/GetAllDatos",
        data: { codigo: token },
    success: function (response) {
      //console.log(response);
      $.each(response, function (i, v) {
          var apellido;
          
          if (v.apellidos == null) {
              apellido = "";
          } else {
              apellido = v.apellidos;
          }
          $("#imguser").attr("src", v.imagen);
          $("#nombreuserspan").html(v.nombres + " " + apellido);
          $("#nombreUser").html('<strong>' + v.nombres + " " + apellido + '</strong>');
        $('#emailspan').html(v.correo);
        if (v.NombreRol === "Administrador") {
          $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
        }
        if ($("#divclaims") != undefined) {
          $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="' + v.usuario_id + '" />');
          $("#divclaims").append('<input type="hidden" name="codigousuario" id="codigousuario" value="' + v.codigo + '" />');
        }

      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    //console.error(textStatus, errorThrown); // Algo fallo
     window.location.href = baseURL + "/Cuenta";
    //envio a la api errores para que almacene el error
  })
  tablaDatosUsuario();

}


//llamo la api y alimento la tabla
function tablaDatosUsuario() {

    var token = $('#codigospan').text();

  $("#jsGrid").jsGrid({
    height: "auto",
    width: "100%",
    sorting: true,
    paging: true,
    autoload: true,
    inserting: false,
    editing: true,
    pageSize: 10,
    //filtering: true,
    controller: {
      loadData: function (filter) {
        var data = $.Deferred();
        $.ajax({
          type: "GET",
          url: baseURL + "api/Usuarios/GetAllDatos",
          data: { codigo: token },
        }).done(function (response) {
          data.resolve(response);
        }).fail(function () {
          swal("error datos usuario");
        });
        return data.promise();
      },
      updateItem: function (item) {
        return $.ajax({
          type: "PUT",
          url: baseURL+"api/Usuarios/PutUpdateDatosUser",
          data: item,
        }).done(function (response) {
          //data.resolve(response);
          swal({
            title:'',
            text: response.mensaje,
            type: response.tipo
          }).then((result) => {
            location.reload();
          });

        }).fail(function () {
          swal("error actualizando usuario");
        });

      },

    },
    fields: [
      
      { name: "usuario_id", type: "text",validate: "required",css: "hide" },
      { name: "codigo", title: "Código", type: "text", validate: "required", css: "hide"},
      { name: "nombres", title: "Nombres", type: "text",validate: "required" },
      { name: "apellidos", title: "Apellidos", type: "text" },
      { name: "telefono", title: "Telefono", type: "text" },
      { name: "razon_social", title: "Razon social", type: "text", validate: "required"  },
      { name: "correo", title: "Correo", type: "text", width: 250,validate: "required" },
      {
        name: "imagen", title: "Avatar",
        itemTemplate: function (val, item) {
          return $("<img>").attr("src", val).css({ height: 100, width: 100 }).on("click", function () {
            openModalImagen(val);
          });
        },

        align: "center",
        validate: "required"
      },
      {type: "control", deleteButton: false  }
    ]
  });

  //validate
  $("#detailsForm").validate({
    rules: {
        name: "required",
        age: { required: true, range: [18, 150] },
        address: { required: true, minlength: 10 },
        country: "required"
    },
    messages: {
        name: "Please enter name",
        age: "Please enter valid age",
        address: "Please enter address (more than 10 chars)",
        country: "Please select country"
    },
    submitHandler: function() {
        formSubmitHandler();
    }
});


}
//funcion abre el modal para cambiar la imagen
function openModalImagen(src) {

  var modalButtonOnly = new tingle.modal({
    closeMethods: [],
    footer: true,
    stickyFooter: true
  });
  //var btn5 = document.querySelector('.tingle_addon');
  //btn5.addEventListener('click', function(){
  modalButtonOnly.open();
  //});
  //modalButtonOnly.setContent(document.querySelector('.tingle_addon_window').innerHTML);
  modalButtonOnly.setContent($('.tingle_addon_window').html());

  modalButtonOnly.addFooterBtn('Cambiar Avatar', 'waves-effect waves-light btn green darken-4', function () {
    //aqui logica para guardar
    updateAvatar();
    modalButtonOnly.close();
  });

  modalButtonOnly.addFooterBtn('Cancelar', 'waves-effect waves-light btn red tingle-btn--pull-right', function () {
    //aqui logica para cancelar
    modalButtonOnly.close();
  });
  if (document.getElementById("formuserimage") != undefined || document.getElementById("formuserimage") != null) {

    //$("#imagePreview").append('<input type="file" id="bla" class="dropify" data-default-file="'+v.ImagenUsuario+'">'); //esta linea la puse xq la libreria no reconoce el primer nodo no eliminar
    //$("#avatar").attr('data-default-file', v.ImagenUsuario);
    $("#avatar").dropify({
      "defaultFile": src,
      "messages": {
        default: 'Arrastre un archivo o haga clic aquí',
        replace: 'Arrastre un archivo o haga clic en reemplazar',
        remove: 'Eliminar',
        error: 'Lo sentimos, el archivo demasiado grande'
      },
    });

    //$("#formuserimage").append('<input type="file" id="avatar" class="dropify" data-default-file="'+v.ImagenUsuario+'">');
  }

}

// funcion actualiza la imagen
function updateAvatar() {

  var formd = $('#formuserimage')[0];
  var data = new FormData(formd);

  $.ajax({
    type: "POST",
    url: baseURL + "api/Usuarios/PostUploadAvatar",
    data: data,
    processData: false,
    contentType: false,
    beforeSend: function() {    
      swal({
        title: 'Espere..',
        text: 'Enviando Avatar por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
        animation: false,
        customClass: 'animated tada',
        allowOutsideClick: false,
        onOpen: () => {
          swal.showLoading()
        }
      })
    },
    success: function (response) {
      
      $.each(response, function (i, v) {
          
        titulo = v.type;
          swal({
            title: titulo,
            text: v.value,
            type: v.type
          }).then((result) => {
            location.reload();
          });

      });
    }

  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo
    //window.location.href = "/Cuenta";
    //envio a la api errores para que almacene el error
  })

}
