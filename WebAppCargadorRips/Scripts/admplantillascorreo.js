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
    tablaDatosUsuario()
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
    rowClick: function (args) {
        showDetailsDialog("Editar", args.item);
    },
    //filtering: true,
    controller: {
      loadData: function (filter) {
        var data = $.Deferred();
        $.ajax({
          type: "GET",
          url: baseURL + "api/Correo/ListarPlantillasCorreo",
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
            url: baseURL +"api/Correo/PutUpdate",
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
      
        { name: "IdPlantilla_Correo", type: "text",validate: "required",css: "hide" },
        { name: "NombrePlantilla_Correo", title: "Nombre", type: "text",validate: "required"},
        { name: "DescripcionPlantilla_Correo", title: "Descripción", type: "text", validate: "required" },
        { name: "AsuntoPlantilla_Correo", title: "Asunto Correo", type: "text",validate: "required" },
        { name: "CuerpoPlantilla_Correo", title: "Cuerpo Correo", type: "text", validate: "required", width: 300 },
        { name: "NombreEstado", title: "Estado", type: "text", validate: "required" },
        {
            type: "control", deleteButton: false,
            modeSwitchButton: false,
            editButton: false,
            headerTemplate: function () {
                return $("<button>").attr("type", "button").text("+").addClass('btn-floating btn-small waves-effect waves-light green')
                    .on("click", function () {
                        showDetailsDialog("Nueva", {});
                    });
            }
        }
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


function showDetailsDialog(dialogType, client) {

    var modal = new tingle.modal({
        closeMethods: [],
        footer: true,
        cssClass: ['tingle-modal--overflow'],
        //stickyFooter: true
    });
    
    modal.open();

    modal.setContent($('#dialogPlantilla').html());

    modal.addFooterBtn('Actualizar', 'waves-effect waves-light btn green darken-4 col s12', function () {
        //aqui logica para guardar
        //updatePlantilla();
        modal.close();
    });

    modal.addFooterBtn('Cancelar', 'waves-effect waves-light btn red tingle-btn--pull-right col s12', function () {
        //aqui logica para cancelar
        modal.close();
    });

    $("#tipomodal").text(dialogType.toString());
    $("#IdPlantilla_Correo").val(client.IdPlantilla_Correo);
    $("#NombrePlantilla_Correo").val(client.NombrePlantilla_Correo);
    $("#DescripcionPlantilla_Correo").val(client.DescripcionPlantilla_Correo);
    $("#AsuntoPlantilla_Correo").val(client.AsuntoPlantilla_Correo);
    $("#CuerpoPlantilla_Correo").val(client.CuerpoPlantilla_Correo);
    //$("#FK_Estado_Plantillas_Correo").val(client.FK_Estado_Plantillas_Correo);
    $("#fkEstado option").filter(function () {
        return this.text == client.FK_Estado_Plantillas_Correo;
    }).attr('selected', true);
    
    //inicializo  los componentes requeridos
    $("#fkEstado").material_select();
    modal.isOverflow();   
    
}