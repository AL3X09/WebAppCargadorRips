/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806

$(document).ready(function () {

    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "Please check your input."
    );

  $( "#formusercontrasenia" ).submit(function( event ) {
    
      event.preventDefault();
  }).validate({
      //debug: true,
      errorClass: "invalid form-error",
      validClass: "valid",
      rules: {
          contrasenia: {
              required: true,
              regex: /(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*\W){8}.*$/,
              minlength: 8,
          },
          contraseniaconfirm: {
              required: true,
              equalTo: "#contrasenia"
          },

      },
      errorElement: 'div',
     
      errorPlacement: function (error, element) {
          var placement = $(element).data('error');
          if (placement) {
              $(placement).append(error)
          } else {
              error.insertAfter(element);
          }
      },
      //For custom messages
      messages: {
          contrasenia: {
              required: "El campo es obligatorio",
              minlength: "Ingrese mínimo 8 caracteres",
              regex: "Ingrese mayúsculas, minúsculas y caracteres especiales"
          },
          contraseniaconfirm: {
              required: "El campo es obligatorio",
              equalTo: "La Contraseña y su comfirmación no coinciden"
          },
      },
      submitHandler: function (e) {
          updateContrasenia();
      }
   
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
          $("#imguser").attr("src", baseURL + v.imagen);
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
      window.location.href = baseURL + "Cuenta";
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
          window.location.href = baseURL +"Home";
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
    type: "POST",
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
                  window.location.href = baseURL +"Home";
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
