﻿/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806

$(document).ready(function () {



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
                if (v.apellidos == null) {
                    apellido = "";
                } else {
                    apellido = v.apellidos;
                }
                $("#imguser").attr("src", v.imagen);
                $("#nombreuserspan").html(v.nombres + " " + apellido);
                $('#emailspan').html(v.correo);
                if (v.nombre_rol === "Administrador") {
                  $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
                }
                if ($("#divclaims")!=undefined) {
                  $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="'+v.usuario_id+'" />');  
                }
        
                if (document.getElementById("divtabEstado")!=undefined || document.getElementById("divtabEstado")!=null) {
                  cantidadRipsCargados(v.usuario_id);
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

