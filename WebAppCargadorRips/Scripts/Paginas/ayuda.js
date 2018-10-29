/**
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
            $('#emailspan').html(v.correo);
            if (v.nombre_rol === "Administrador") {
              $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
            }
            if ($("#divclaims")!=undefined) {
              $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="'+v.IdUsuario+'" />');  
            }
    
            /*
                if (document.getElementById("divtabEstado") != undefined || document.getElementById("divtabEstado") != null) {
              cantidadRipsCargados(v.usuario_id);
            }
            */
    
          });
    
        }
      }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        window.location.href = "/Cuenta";
        //envio a la api errores para que almacene el error
      })
  //
  getPreguntasFrecuentes();
}

//llamo la api y alimento el ul de pregunta frecuente
function getPreguntasFrecuentes() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Preguntas/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listpreguntas').append(
                        '<li>'+
                            '<div class="collapsible-header blue "><span class="white-text text-darken-2">' + v.pregunta_frecuente +'</span></div>'+
                            '<div class="collapsible-body">'+
                                '<ul>'+
                                    '<li>' + v.respuesta_preguntas +'</li>'+
                                '</ul>'+
                            '</div>'+
                        '</li>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        swal(
            '',
            "Error al intertar traer las preguntas frecuentes",
            'error'
        );
       
        //envio a la api errores para que almacene el error
    })
    getPresentaciones();
}

//llamo la api y alimento el ul de presentaciones
function getPresentaciones() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Presentaciones/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listpresentaciones').append(
                    '<li class="collection-item"><div>' + v.descripcion + '<a href="' + v.ruta +'" class="secondary-content" target="_blank"><i class="material-icons">send</i></a></div></li>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        swal(
            '',
            "Error al intertar traer las presentaciones",
            'error'
        );

        //envio a la api errores para que almacene el error
    })
    getManuales();
}

//llamo la api y alimento el ul de manuales
function getManuales() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Manuales/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listmanuales').append(
                    '<li class="collection-item"><div>' + v.descripcion + '<a href="' + v.ruta + '" class="secondary-content" target="_blank"><i class="material-icons">send</i></a></div></li>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        swal(
            '',
            "Error al intertar traer las presentaciones",
            'error'
        );

        //envio a la api errores para que almacene el error
    })
    getNormatividad();
}

//llamo la api y alimento el ul de resoluciones y normas
function getNormatividad() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Normatividad/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listresolucionesnormas').append(
                    '<li class="collection-item"><div>' + v.descripcion + '<a href="' + v.ruta + '" class="secondary-content" target="_blank"><i class="material-icons">send</i></a></div></li>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        swal(
            '',
            "Error al intertar traer las Resoluciones y normas",
            'error'
        );

        //envio a la api errores para que almacene el error
        })
    getContactos();
}

//llamo la api y alimento las tarjetas de contactos(usuarios administradores)
function getContactos() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Usuarios/ListarContactos",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#divcontactos').append(
                    '<div class="col s12 m4" >'+
                    '<div class="card">' +
                    '<div class="card-image">' +
                    '<img class="responsive-img" src="'+v.imagen+'">'+
                    '<span class="card-title">' + v.nombre+' '+v.apellido +'</span>'+
                    '</div>' +
                    '<div class="card-content">' +
                    '<p>Descripcón:</p>'+
                    '<p>' + v.descripcion + '</p>' +
                    '<div class="card-action">' +
                    '<p>Correo:</p>' +
                    '<spam>' + v.correo + '</spam>' +
                    '</div>' +
                    '<div class="card-action">' +
                    '<p> EXT:' + v.extencion + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div >')
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        swal(
            '',
            "Error al intertar traer la informacion del equipo",
            'error'
        );

        //envio a la api errores para que almacene el error
    })

}