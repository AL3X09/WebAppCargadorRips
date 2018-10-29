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
        //if (document.getElementById("divtabEstado")!=undefined || document.getElementById("divtabEstado")!=null) {
        ripscargadostodosanios(v.IdUsuario);
        //}
        
      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    //console.error(textStatus, errorThrown); // Algo fallo
    window.location.href = "/Cuenta";
    //envio a la api errores para que almacene el error
  })
  //
  //ripscargadosyestados();
}

// pinto 
function ripscargadostodosanios(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/Listarcantidadanios',
        method: 'GET',
        data: {iduser:token},
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estado = [];
            var total = [];

            $.each(data, function (k, v) {
                if (v.Cantidad == 0) {
                    v.Anio = "Sin definir"
                }
                
                Estado.push(v.Anio);
                total.push(v.Cantidad);
            });

            var chartdata = {
                labels: Estado,
                datasets: [
                    {
                        borderWidth: 1,
                        backgroundColor: [
                            window.chartColors.silver,
                            window.chartColors.orange,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.redclear,
                            window.chartColors.orangeclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.silverclear
                        ],
                        borderColor: [
                            window.chartColors.silver,
                            window.chartColors.orange,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.redclear,
                            window.chartColors.orangeclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.silverclear
                        ],
                        hoverBackgroundColor: [
                            window.chartColors.silver,
                            window.chartColors.orange,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.redclear,
                            window.chartColors.orangeclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.silverclear

                        ],
                        hoverBorderColor: [
                            window.chartColors.silver,
                            window.chartColors.orange,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.redclear,
                            window.chartColors.orangeclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.silverclear
                        ],
                        data: total
                    }
                ]
            };

            var ctx = $("#canvascantidadestadoripscargadostodosanios");
            //options
            var option = {

                responsive: false,
                title: {
                    display: true,
                    position: "top",
                    text: "Cantidad RIPS cargados por años",
                    fontSize: 10,
                },
                legend: {
                    display: true,
                    position: "bottom",
                    labels: {
                        fontSize: 10
                    }
                }

            };


            var barGraph = new Chart(ctx, {
                type: 'pie',
                data: chartdata,
                options: option
            });
        },
        error: function (data) {
            console.log(data);
        }
    });

    ripscargadosyestados(token);
}

function ripscargadosyestados(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/Listarxestados',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
            $.each(data, function (k, v) {
                Estados.push(v.Estado);

                total.push(v.Cantidad);
                if (k == 0) {
                    color = window.chartColors.green
                } else if (k == 1) {
                    color = window.chartColors.blue
                } else if (k==2){
                    color=window.chartColors.red
                }
                datos.push({
                    label: v.Estado.toString(),
                    backgroundColor: color,
                    fillColor: window.chartColors.blue,
                    strokeColor: color,
                    data: [v.Cantidad]
                });
                //ELIMINAR DUPLICADO DE UN ARRAY
                //if ($.inArray(v.Descripcion_Zona, Zonas) === -1) Zonas.push(v.Descripcion_Zona);
                //Separo los distritos y creo un nuevo array
                //var distr = v.Descripcion_Distrito;
                //var Distritos1 = distr.split(",");
                //Distritos = $.merge( $.merge( [], Distritos ), Distritos1 );

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: [""],
                datasets: datos
            }

            var ctx = document.getElementById("canvascantidadestadoripscargadosxestados").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    title: {
                        display: true,
                        text: "Estado RIPS cargados para el año " + (new Date()).getFullYear()
                    }, 
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Estado'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Cantidad'
                        }
                    }]
                },
                    responsive: false,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    
}
