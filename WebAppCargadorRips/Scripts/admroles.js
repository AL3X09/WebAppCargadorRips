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
  tablaDatosUsuario()
}

//llamo la api y alimento la tabla
function tablaDatosUsuario() {

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
    //filtering: true,
    controller: {
      loadData: function (filter) {
        var data = $.Deferred();
        $.ajax({
          type: "GET",
          url: baseURL + "api/Roles/Listar",
          data: filter,
        }).done(function (response) {
          data.resolve(response);
        }).fail(function () {
          swal("error lista de roles");
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
          swal("error actualizando roles");
        });

      },

    },
    fields: [
      
      { name: "IdRol", type: "text",validate: "required",css: "hide" },
      { name: "NombreRol", title: "Nombre", type: "text",validate: "required"},
      { name: "NombreEstado", title: "Estado", type: "text",validate: "required" },
      //{type: "control", deleteButton: false  }
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