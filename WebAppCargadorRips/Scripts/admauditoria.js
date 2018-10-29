/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806

$(document).ready(function () {
  
  jsGrid.locale("es");

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
            
            if (v.ApellidosUsuario == null) {
                apellido = "";
            } else {
                apellido = v.ApellidosUsuario;
            }
            $("#imguser").attr("src", v.ImagenUsuario);
            $("#nombreuserspan").html(v.NombresUsuario + " " + apellido);
        $('#emailspan').html(v.CorreoUsuario);
        if (v.NombreRol === "Administrador") {
          $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
        }
        if ($("#divclaims")!=undefined) {
          $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="'+v.IdUsuario+'" />');  
        }

        if (document.getElementById("divtabEstado")!=undefined || document.getElementById("divtabEstado")!=null) {
          cantidadRipsCargados(v.IdUsuario);
        }
        
      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    //console.error(textStatus, errorThrown); // Algo fallo
    window.location.href = "/Cuenta";
    //envio a la api errores para que almacene el error
  })
  //
  tablaDatosAuditorias();
  getAllRoles();
}

//funcion llamo los roles
function getAllRoles() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Roles/Listar",
        success: function (response) {
            var option = $('<option></option>').attr("value", "").text("Seleccione...");
            $.each(response, function (i, v) {

                option = $('<option></option>').attr("value", v.IdRol).text(v.NombreRol);
                $("#fkRol").append(option);
            });
            //console.log(option);

            $("#fkRol").material_select();

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
        swal(
            textStatus,
            'Hay un problema al traer los datos de los roles comunícate con el administrador ',
            'error',
        )
        //window.location.href = "/Cuenta";
        //envio a la api errores para que almacene el error
    })
    //
    getAllEstados();
}
//funcion llamo los estados
function getAllEstados() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Estados/Listar",
        success: function (response) {

            var option = $('<option></option>').attr("value", "").text("Seleccione...");
            $.each(response, function (i, v) {

                option = $('<option></option>').attr("value", v.id).text(v.nombre);
                $("#fkEstado").append(option);
            });
            $("#fkEstado").material_select();
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
        //envio a la api errores para que almacene el error
    })
    
}

//llamo la api y alimento la tabla
function tablaDatosAuditorias() {

  $("#jsGrid").jsGrid({
    height: "auto",
    width: "100%",
    sorting: true,
    paging: true,
    autoload: true,
    inserting: false,
    editing: true,
    pageSize: 10,
    pageButtonCount: 5,
    filtering: true,
    rowClick: function (args) {
        showDetailsDialog("Editar", args.item);
    },
    controller: {
      loadData: function (filter) {
        var data = $.Deferred();
        $.ajax({
          type: "GET",
          url: baseURL + "api/Usuarios/Listar",
          data: filter,
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
      
      { name: "IdUsuario", type: "text",validate: "required",css: "hide" },
      { name: "CodPrestadorUsuario", title: "Código", type: "text",validate: "required"},
      { name: "NombresUsuario", title: "Nombres", type: "text",validate: "required" },
      { name: "ApellidosUsuario", title: "Apellidos", type: "text",validate: "required" },
      { name: "TelefonoUsuario", title: "Telefono", type: "text" },
      { name: "RazonSocialUsuario", title: "Razon social", type: "text" },
      { name: "CorreoUsuario", title: "Correo", type: "text", width: 250,validate: "required" },
      {
        name: "ImagenUsuario", title: "Avatar",
        itemTemplate: function (val, item) {
          return $("<img>").attr("src", val).css({ height: 100, width: 100 }).on("click", function () {
            //openModalImagen(val);
          });
        },

        align: "center",
        validate: "required"
      },
      {
          type: "control", deleteButton: false,
          modeSwitchButton: false,
          editButton: false,
          headerTemplate: function () {
              return $("<button>").attr("type", "button").text("+").addClass('green')
                  .on("click", function () {
                      showDetailsDialog("Agregar", {});
                  });
          }
      }
    ]
  });



}

function showDetailsDialog(dialogType, client) {

    
    
    
    var modal = new tingle.modal({
        closeMethods: [],
        footer: true,
        cssClass: ['tingle-modal--overflow'],
        //stickyFooter: true
    });

    modal.open();

    modal.setContent($('#dialogPlantilla').html());

    modal.addFooterBtn(dialogType.toString(), 'waves-effect waves-light btn green darken-4 col s12', function () {
        //aqui logica para guardar
        enviardatos();
        modal.close();
    });

    modal.addFooterBtn('Cancelar', 'waves-effect waves-light btn red tingle-btn--pull-right col s12', function () {
        //aqui logica para cancelar
        modal.close();
    });

    $("#tipomodal").text(dialogType.toString());
    $("#IdUsuario").val(client.IdUsuario);
    $("#CodPrestadorUsuario").val(client.CodPrestadorUsuario);
    $("#NombresUsuario").val(client.NombresUsuario);
    $("#ApellidosUsuario").val(client.ApellidosUsuario);
    $("#CorreoUsuario").val(client.CorreoUsuario);
    $("#RazonSocialUsuario").val(client.RazonSocialUsuario);
    $("#TelefonoUsuario").val(client.TelefonoUsuario);
    $("#fkRol option").filter(function () {
        return this.text == client.NombreRol;
    }).attr('selected', true);
    $("#fkEstado option").filter(function () {
        return this.text == client.NombreEstado;
    }).attr('selected', true);
    
    $("#fkRol").material_select();
    $("#fkEstado").material_select();
    modal.isOverflow();

}
//envio los datos del formulario
function enviardatos() {
    var url
    if ($("#IdUsuario").val() != "" || $("#IdUsuario").val() > 0) {
        url = baseURL + "api/Usuarios/PutUpdateDatosUser";
    } else {
        url = baseURL + "api/Usuarios/PostInsertNewDatosUser";
    }

    $.ajax({
        type: "PUT",
        url: url,
        data: $('#formusuarios').serialize(),
        success: function (response) {

            swal({
                title: '',
                text: response.mensaje,
                type: response.tipo
            }).then((result) => {
                //window.location.href = "/Home";
                window.location.reload();
            });

        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
        swal(
            'error comunicate con el administrador',
        ).then((result) => {
            window.location.reload();
        });
        //envio a la api errores para que almacene el error
    })

}