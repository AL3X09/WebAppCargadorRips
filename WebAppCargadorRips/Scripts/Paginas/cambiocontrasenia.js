/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806

$(document).ready(function () {

  $( "#formusercontrasenia" ).submit(function( event ) {
    
    event.preventDefault();
    updateContrasenia();
  });

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
        if (v.nombre_rol === "Administrador") {
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
    window.location.href = "/Cuenta";
    //envio a la api errores para que almacene el error
  })


}

//metodo cancela el bufer y retorna a inicio
function cancelado() {
  swal({
    title: '¿Estas seguro?',
    text: 'Desea cancelar la operación',
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Si, Cancelar',
    cancelButtonText: 'No',
    confirmButtonClass: 'btn green',
    cancelButtonClass: 'waves-effect waves-light btn red',
  }).then(function () {
    swal(
      'Cancelado',
      'Operación Cancelada',
      'error',
      setTimeout(function () {
        //location.reload("/Home/Index");
        window.location.href = "/Home";
      }, 2000)
    )
  })
}
// Metodo valida los campos
function check(input) {

  if (input.value != $('#contrasenia').val()) {
    input.setCustomValidity('La Contraseña y su comfirmación no coinciden.');
  } else {
    // input is valid -- reset the error message
    input.setCustomValidity('');
  }
}

// funcion actualiza la imagen
function updateContrasenia() {

  $.ajax({
    type: "PUT",
    url: baseURL + "api/Usuarios/PutUpdateContrasenia",
    data: $('#formusercontrasenia').serialize(),
    success: function (response) {
        console.log(response);
          swal({
            title: '',
            text: response.mensaje,
            type: response.tipo
          }).then((result) => {
              if (response.codigo == 201) {
                  $('#acerrarsesion').click();
              } else {
                  window.location.href = "/Home";
              }
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
