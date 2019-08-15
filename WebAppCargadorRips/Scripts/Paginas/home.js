/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de la pagina home y la pagina carga de rips
 * 26-10-2017
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806
var nombre = []; //variable almacena los nombre de los archivos a cargar
var TipoEstructuraArray = []; //variable que almacena las estructuras existentes en la norma 
var currentDate1 = new Date();
var currentDate2 = new Date()
var minday = new Date();
var maxday = new Date();
//obtengo la fecha minima para enviarla al calendario
//(Anio-3),(mes),(dia)
minday = new Date(currentDate1.getFullYear() - 4, 0, 1);
//(anio),(mes+7),(dia)
//maxday = new Date(currentDate2.getFullYear() - 1, currentDate2.getMonth() + 7, 0);
maxday = new Date(currentDate2.getFullYear(), currentDate2.getMonth()-0, 0);

//creo un vector que me permite almacenar la configuracion para el calendario
const daysOfYear = [];
const EnddaysOfYear = [];

var reco;//defino una variable de recorrido
//RECORRO las fechas obtengo los meses transcurridos dentro de estas
//ojo esta es la que arma los array con fechas permitidas
for (var d = minday; d <= (new Date(currentDate2.getFullYear(), currentDate2.getMonth()+1, 0)); d.setMonth(d.getMonth() + 1)) {
  //console.log(d);
  daysOfYear.push(d.toLocaleDateString('zh-Hans-CN').split("/"));
  reco = (d.getFullYear() + "-" + (d.getMonth()) + "-" + "0").split("-");
  EnddaysOfYear.push(reco);
  //EnddaysOfYear.push([d.getFullYear()+"-"+d.getMonth()+"-"+"0"]);
}

//le agrego al inicio del arrary un valor de configuración para el calendario
daysOfYear.unshift(true);
EnddaysOfYear.unshift(true);

$(document).ready(function () {

  //valido que el explorador soporte la api de lectura de archivos
  if (!(window.File)) {
    //console.log('La API File no está soportada en este esplorador');
    swal(
      'Precaución',
      'Su explorador no soporta la lectura de archivos, por favor cambie a un explorador mas reciente o active la función si la desactivado',
      'info'
    )
    return;
  }


  //inicializo modals en cascada
  /*
  $("#modalCarga").modal();
  $('#modalPreloader').modal();
  $('#modalFaltanArchivosaCargar').modal();
  $('#modalResidenciaUsuarios').modal();
  */


  /*
  * FUNCIONES ANONIMAS 
  */

  //escucho select tipousuario para efectuar respectivas acciones
  $('#tipoUsuario2').change(function (event) {


    /*
    se incativa por que el cliente opto por subir los archivos de manera periodica
    if ($(this).val() == 1) {
      //muestro div semestre
      $('#opcmensual').prop('disabled', true);
      $('#opcmensual').prop('checked', false);
      $('#opcsemestral').prop('checked', true);
      $('#opcsemestral').prop('disabled', false);
      $('#DIVsemestral').removeClass("hide").addClass("show");
      $('#DIVmensual').hide(1000);
    } else {
      //oculto div semestre
      $('#opcmensual').prop('disabled', false);
      $('#opcmensual').prop('checked', true);
      $('#opcsemestral').prop('checked', false);
      $('#opcsemestral').prop('disabled', true);
      //$('#DIVsemestral').hide(1000);
      $('#DIVsemestral').removeClass("show").addClass("hide");
      $('#DIVmensual').show();
    }
    */
    var res = $(this).val().substring(4,5);
    // llamo a la funcion categoria y le envio el tipo de usuario seleccionado y el id del tipo de usuario
    //con un +1 ya que no lista un registro ener cuidado con los registros
    callCategoria($(this).val(),parseInt(res)+1);

  });

  //escucho valor del campo check IVE para efectuar respectivas acciones
  $('#IVE').change(function (event) {
    if ($(this).is(':checked')) {
      deshabilitarDatosGer();

    } else {
      habilitarDatosGer();
    }
  });
  //escucho valor del campo check NO POS para efectuar respectivas acciones
  $('#NOPOS').change(function (event) {
    if ($(this).is(':checked')) {
      //Envio el valor de subsidiado al select de tipo usuario y categoria con general
      $('#tipoUsuario2').val('tipo2');
      //dahabilito los select
      deshabilitarDatosGer2();
      
    } else {
      //habilito los selects
      habilitarDatosGer2();
        $('#tipoUsuario2').val('');
        $('#categoria').append(new Option("Seleccione...", ''));
        $('#categoria').val('');
        $("select").material_select();
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
        $('#emailspan').html(v.correo);
        if (v.nombre_rol === "Administrador") {
          $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
        }
        if ($("#divclaims")!=undefined) {
          $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="'+v.usuario_id+'" />');  
        }

        if (document.getElementById("divtabEstado")!=undefined || document.getElementById("divtabEstado")!=null) {
          cargaratabla(v.usuario_id);
        }
        
        // si el prestador no esta habilitado notifico visualmente
        if (v.habilitado === false) {
          
          iziToast.warning({
            title: 'Alerta',
            message: 'Usted se encuentra Inhabilitado en REPS!!',
            position: 'topCenter',
            timeout: 50000,
            resetOnHover: true,
            drag: true,
            close: true,
          });
        }

      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    //console.log(baseURL);
    window.location.href = baseURL + "Cuenta";
    console.error(textStatus, errorThrown); // Algo fallo
    //envio a la api errores para que almacene el error
  })

  
}

// TODO YA NO SE USA EN ESTE SCRIPT funcion trae la informacion de los archivos cargados
// funcion trae la informacion de los archivos cargados
/*
function cantidadRipsCargados(token) {

  $.ajax({
    type: "GET",
    url: baseURL + "api/Rips/GetCantidad",
    data: { fktoken: token },
    success: function (response) {

      $.each(response, function (i, v) {

        if (v.cantidadCargados > 0) {
          estadoRipsCargados(token);
        }
        else {
          $("#DIVestado").html('<div class="row ">'+
            '<div class="col s12 m5 offset-m4 valign">'+
                '<div class="card">'+
                    '<div class="card-image">'+
                        '<img class="activator" src="/img/status/error.gif">'+
                        '<span class="card-title">No tiene archivos...</span>'+
                    '</div>'+
                '</div>'+
            '</div>'+
        '</div>'+
        '<div class="row">'+
          '<div class="col s12">'+
            '<blockquote>'+
                'No posee RIPS, <strong>CARGADOS </strong>'+
            '</blockquote>'+
          '</div>'+
        '</div>');
        }
        
      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo   
    
  })
}
// TODO YA NO SE USA EN ESTE SCRIPT funcion trae la informacion de los archivos cargados
function estadoRipsCargados(token) {
  
  $.ajax({
    type: "GET",
    url: baseURL + "api/Rips/GetEstado",
    data: { fktoken: token },
    success: function (response) {
      
      $.each(response, function (k, v) { 
         
        $("#DIVestado").html('<div class="row ">'+
        '<div class="col s12 m5 center-align">'+
            '<div class="card">'+
                '<div class="card-image">'+
                    '<img class="activator" src="'+v.imgEstado+'">'+
                    '<span class="card-title">'+v.nombreEstado+'</span>'+
                '</div>'+
            '</div>'+
        '</div>'+
        '</div>'+
        '<div class="row">'+
          '<div class="col s12">'+
            '<blockquote>'+
                'El estado de tús RIPS, para el periodo del reporte comprendido entre las fechas <strong>'+v.periodoinicio.substring(0, 10)+'</strong> al <strong>'+v.periodofin.substring(0, 10)+'</strong> es '+
                '<strong>'+v.nombreEstado.toUpperCase()+'</strong>, esto queire decir que '+v.leyendaEstado+
            '</blockquote>'+
          '</div>'+
        '</div>');

      });

      }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo   
    
  })
}
*/


//muestro el div oculto
function inicilizoDiv() {
  $('#DIVmensual').show();

}

//envio a los datepicker para especificarles fechas unicas
function inicilizoDatepicker() {
  //iniciaizo el de fecha inicio para que tome siempre el primer dia del mes
  //console.log(new Date(currentDate1.getFullYear() - 2, 0, 1));
  //console.log(maxday);
  var inputi = $("#fechaInicio").pickadate({
    selectMonths: true,
    selectYears: true,
    format: 'dd/mm/yyyy',
    formatSubmit: 'yyyy/mm/dd',
    hiddenName: true,
    min: new Date(currentDate1.getFullYear() - 3 ,0, 1),//new Date(currentDate1.getFullYear()-2,0, 1),
    max: maxday,
    disable: daysOfYear,
      onSet: function (e) {
          //OJO AQUI SETEA LA FECHA PARA ENVIARLA AL CAMPO FECHA
          var fec = inputi.get();
          var fec1 = moment(fec[0].value).format('YYYY-DD-MM');
          var fec2 = moment(fec1).endOf('month').format('YYYY/MM/DD');//moment(fec1).add('months', 1).date(0);
          //console.log(fec1); 
          //moment(fec1).endOf('month');
          //console.log(new Date(fec2)); 
          //console.log((fec[0].value).toString());
          //console.log(new Date(fec1.getFullYear(), fec1.getMonth() + 1, 0));
          var inputf = $('#fechaFin').pickadate('picker');
          inputf.set('select', new Date(fec2));
          //console.log(dateText);          
      }

  });
  //iniciaizo el de fecha inicio para que tome siempre el primer dia del mes
    var $input = $("#fechaFin").pickadate({
        selectMonths: true,
        selectYears: true,
        format: 'dd/mm/yyyy',
        formatSubmit: 'yyyy/mm/dd',
        hiddenName: true,
        min: new Date(currentDate1.getFullYear() - 3, 0, 1),//new Date(currentDate1.getFullYear() - 2, currentDate1.getMonth() - 1, 1),
        max: maxday,
        disable: EnddaysOfYear
  });

}

//Inicioalizo el campo del multifile
function inicilizoMultifile() {
  $('#rips').MultiFile({
    accept: 'txt',
    onFileSelect: function (element, value, master_element) {
      $('#rips').MultiFile("reset");
    },
    afterFileSelect: function (element, value, master_element) {

    },
    onFileRemove: function (element, value, master_element) {

    },
    onFileDuplicate: function (element, value, master_element) {
      $('#F9-Log').append('<li>onFileDuplicate - ' + value + '</li>')
    },
    onFileTooMany: function (element, value, master_element) {
      $('#F9-Log').append('<li>onFileTooMany - ' + value + '</li>')
    },
    onFileTooBig: function (element, value, master_element) {
      $('#F9-Log').append('<li>onFileTooBig - ' + value + '</li>')
    }
  });
}

function cancelado() {
  swal({
    title: '¿Estas seguro?',
    text: 'Desea cancelar la operación de carga',
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

//efectuo acciones necesarias para el buen funcionamiento de la aplicación
function deshabilitarDatosGer() {
  $('#tipoUsuario2').prop('disabled', true);
  $('#categoria').prop('disabled', true);
  $("select").material_select();
  $('#NOPOS').prop('disabled', true);
  $('#fortipo').addClass("hide");
  $('#forcate').addClass("hide");
  //callTipoUsuario();
}
//efectuo acciones posteriores 
function habilitarDatosGer() {
  $('#tipoUsuario2').prop('disabled', false);
  $('#categoria').prop('disabled', false);
  $("select").material_select();
  $('#NOPOS').prop('disabled', false);
  $('#fortipo').removeClass("hide");
  $('#forcate').removeClass("hide");
  
}
//efectuo acciones necesarias para el buen funcionamiento de la aplicación
function deshabilitarDatosGer2() {
  $('#tipoUsuario2').prop('disabled', true);
  $('#categoria').prop('disabled', true);
  $("select").material_select();
  $('#IVE').prop('disabled', true);
  $('#fortipo').addClass("hide");
  $('#forcate').addClass("hide");
  
}
//efectuo acciones posteriores 
function habilitarDatosGer2() {
  $('#tipoUsuario2').prop('disabled', false);
  $('#categoria').prop('disabled', false);
  $("select").material_select();
  $('#IVE').prop('disabled', false);
  $('#fortipo').removeClass("hide");
  $('#forcate').removeClass("hide");
  
}
//valido que no existan archivos con el mismo nombre
function validandonoduplicados() {
  $('#modalPreloader').modal('open');

  setTimeout(function () {
    $('#modalPreloader').modal('close');
    faltanxcargararchivos();
  }, 3000);
}
//valido que archivos que se van a cargar respecto a api la consumir
function faltanxcargararchivos() {
  $('#modalFaltanArchivosaCargar').modal('open', {
    dismissible: false
  });

  setTimeout(function () {
    $('#modalFaltanArchivosaCargar').modal('close');
    validandorestructura();
  }, 3000);
}
//valido que la estructura de los archivos corresponda a la requerida por la api
function validandorestructura() {
  $('#modalPreloader').modal('open');

  setTimeout(function () {
    $('#modalPreloader').modal('close');
    residenciausuariosarchivo();
  }, 3000);
}
//presento interfaz de los usuarios encontrados en el archivo US
function residenciausuariosarchivo() {
  $('#modalResidenciaUsuarios').modal('open', {
    dismissible: false
  });


}

//presento interfaz de los usuarios encontrados en el archivo US
function valoresfacturacionarchivo() {
  $('#modalResidenciaUsuarios').modal('close');
  $('#modalValoresFacturacion').modal();
  $('#modalValoresFacturacion').modal('open', {
    dismissible: false
  });

}

function mostrarFormCargueRips() {
  $('#DIVformCargaRips').removeClass('hide').addClass('show');
  $('#DIVestado').removeClass('show').addClass('hide');
  $('#DIVtablacargados').removeClass('show').addClass('hide');

}
//funcion solicita datos para cargar al select de tipo de usuarios
function callTipoUsuario() {

  $.ajax({
    type: "GET",
    url: baseURL + "api/TipoUsuario/Listar",
    success: function (data) {

      var option = $('<option></option>').attr("value", "").text("Seleccione...");
      $.each(data, function (i, v) {
        
        option = $('<option></option>').attr("value", "tipo" + v.numero).text(v.nombre);
        $("#tipoUsuario2").append(option);
      });
      //console.log(option);

      $("#tipoUsuario2").material_select();
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    console.error(textStatus, errorThrown); // Algo fallo
    swal(
      textStatus,
      'Hay un problema al traer los datos de tipo de usuario comuníquese con el administrador ',
      'error',
      setTimeout(function () {
      }, 2000)
    )
  });

}

//funcion solicita datos para cargar al select de categorias
function callCategoria(objeto, idtipousuario) {
  $('#categoria').empty();

  $.ajax({
    type: "GET",
    url: baseURL + "api/Categorias/Listar",
    data: { 'id': objeto },
    success: function (data) {
      var option = $('<option></option>').attr("value", "").text("Seleccione...");
      $.each(data, function (i, v) {
        option = $('<option></option>').attr("value", v.categoria_id).text(v.nombre);
        $("#categoria").append(option);
      });
      //console.log(option);

      $("#categoria").material_select();
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    console.error(textStatus, errorThrown); // Algo fallo
    swal(
      textStatus,
      'Hay un problema al traer los datos de las Categorías comuníquese con el administrador ',
      'error',
      setTimeout(function () {
      }, 2000)
    )
  });
  $("#tipoUsuario").val(idtipousuario)
}

//funcion envia el archivo
function loadRIPS() {
  //debugger;

  var formd = $('#formulariocargaarchivo')[0];
  var data = new FormData(formd);

  $.ajax({
    url: baseURL+"api/Rips/Upload",
    type: "POST",
    data: data,
    processData: false,
    contentType: false,
    beforeSend: function() {    
      swal({
        title: 'Espere..',
        text: 'Enviando archivos por favor espere.',
        animation: false,
        customClass: 'animated tada',
        //timer: 5000,
        onOpen: () => {
          swal.showLoading()
        }
      });
      },
    success: function (response) {
      $.each(response, function (i, v) { 
          //valido el codigo retornado por la api
          if(v.codigo==201){
            //envio correo
            enviarCorreo(1,v.consec);
          }
          //cambio el tititulo si es diferente de error
          console.log(v.type);
          if(v.type!=="error"){
            //titulo="Mensaje";
          }else{
            titulo=v.type;
            swal(
              titulo,
              v.value,
              v.type
              )
          }
      });
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    console.error(textStatus, errorThrown); // Algo fallo
    swal(
      textStatus,
      'Hay un problema al cargar los datos y archivos comuníquese con el administrador ',
      'error',
      setTimeout(function () {
      }, 2000)
    )
  });
  //debugger;
}

//Función que me permite limpiar los campos
function limpiardivfiles() {
    $('#filename').html("");
    nombre = [];
    zonaarchvos();
  //$('#rips').val('');
  //$('#archivos').val('');
}

//funcion que mantiene la zona de los archivos
function zonaarchvos() {

  var dropZoneId = "drop-zone";
  var buttonId = "clickHere";
  var mouseOverClass = "mouse-over";
  var dropZone = $("#" + dropZoneId);
  var inputFile = dropZone.find("input");
  var finalFiles = [];

  var ooleft = dropZone.offset().left;
  var ooright = dropZone.outerWidth() + ooleft;
  var ootop = dropZone.offset().top;
  var oobottom = dropZone.outerHeight() + ootop;

  document.getElementById(dropZoneId).addEventListener("dragover", function (e) {
    e.preventDefault();
    e.stopPropagation();
    //dropZone.addClass(mouseOverClass);
    var x = e.pageX;
    var y = e.pageY;

    if (!(x < ooleft || x > ooright || y < ootop || y > oobottom)) {
      inputFile.offset({
        top: y - 15,
        left: x - 100
      });
    } else {
      inputFile.offset({
        top: -400,
        left: -400
      });
    }

  }, true);

  if (buttonId != "") {
    var clickZone = $("#" + buttonId);

    var oleft = clickZone.offset().left;
    var oright = clickZone.outerWidth() + oleft;
    var otop = clickZone.offset().top;
    var obottom = clickZone.outerHeight() + otop;

    //aqui se mueve el imput segun posision del mouse
    $("#" + buttonId).mousemove(function (e) {
      var x = e.pageX;
      var y = e.pageY;
      inputFile.offset({
        top: y - 15,
        left: x - 160
      });
      /*if (!(x < oleft || x > oright || y < otop || y > obottom)) {
          inputFile.offset({
              top: y - 15,
              left: x - 160
          });
      } else {
          inputFile.offset({
              top: -400,
              left: -400
          });
      }*/
    });
  }

  document.getElementById(dropZoneId).addEventListener("drop", function (e) {
    $("#" + dropZoneId).removeClass(mouseOverClass);
  }, true);


  inputFile.on('change', function (e) {
    //finalFiles = [];

    $('#filename').html("");

    var fileNum = this.files.length,
      initial = 0,
      counter = 0;

    $.each(this.files, function (idx, elm) {
      finalFiles[idx] = elm;

    });

    for (initial; initial < fileNum; initial++) {
      counter = counter + 1;
      $('#filename').append('<div id="file_' + initial + '"><span class="fa-stack fa-lg"><i class="fa fa-file fa-stack-1x "></i><strong class="fa-stack-1x" style="color:#FFF; font-size:12px; margin-top:2px;">' + counter + '</strong></span> ' + this.files[initial].name + '</div>');
    }
  });

}

function callEstructuraCampo() {

  $.ajax({
    type: "GET",
    url: baseURL + "api/Estructura/Listar",
    success: function (response) {

      $.each(response, function (i, v) {
        TipoEstructuraArray[v.estAcronimo] = v.cantidadDatos;
      });

    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo
  })

}

//Esta funcion envia el correo al cargar los archivos en el servidor
//encontrados
function enviarCorreo(tipoCorreo,consec) {

  var email = $('#emailspan').text();
  var nombreuser = $('#nombreuserspan').text();
  $.ajax({
    type: "POST",
    url: baseURL + "api/Correo/SendEmail",
    data:{ codPlantilla: tipoCorreo, usercorreo:email, usernombre:nombreuser,codigocarga:consec},
    beforeSend: function() {    
      swal({
        title: 'Espere..',
        text: 'Enviando correo por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
        animation: false,
        customClass: 'animated tada',
        allowOutsideClick: false,
        onOpen: () => {
          swal.showLoading()
        }
      }).then((result) => {
          //console.log('cerrado modal')
        
        })
      },
    success: function (response) {
      //console.log(response);
      $.each(response, function (i, v) { 
         //valido el codigo retornado por la api aqui pongo de codigo 1009 ya que es el envio de correo
         if(v.codigo==1009){
          setTimeout(function () {
              window.location.href = baseURL +"Home";
          }, 9000)
        }
        //cambio el tititulo si es diferente de error
        if(v.type!=="error"){
          titulo="Mensaje";
        }else{
          titulo=v.type;
        }
        swal(
          titulo,
          v.value,
          v.type
          )

      });
      
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo
  });

}

//Esta funcion envia el formulario para almacenarlo en la tabla 
//de validacion
function UploadValidacionConErrores(reserrores) {
  
  var formd = $('#formulariocargaarchivo')[0];
  var data = new FormData(formd);

  $.ajax({
    url: baseURL+"api/Rips/GuardarValidacionConErrores",
    type: "POST",
    data: data,
    processData: false,
    contentType: false,
    beforeSend: function() {    
      swal({
        title: 'Espere..',
        text: 'Almacenando información, esto puede tardar unos segundos.',
        animation: false,
        customClass: 'animated tada',
        allowOutsideClick: false,
        onOpen: () => {
          swal.showLoading()
        }
      }).then((result) => {
          //console.log('cerrado modal')
          //window.location.href = "/Home";
        })
      },
    success: function (response) {
      //console.log(response);
      $.each(response, function (i, v) { 
        //cambio el tititulo si es diferente de error
        //console.log(v);
        if(v.type=="error"){
          titulo="Mensaje";
          swal(
            titulo,
            v.value,
            v.type
          ).then((result) => {
            //console.log('cerrado modal')
            //location.reload();
              enviarCorreoErrores(reserrores);
              //window.location.href = baseURL +"Home";
          })
        }
        /*modificado 30-11-2018
         * else {
          //Envio correo de errores
          enviarCorreoErrores(reserrores)
        }*/

      });
      
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo
  });

}

//Esta funcion envia el correo con los diferentes problemas
//encontrados
function enviarCorreoErrores(errores) {
  //console.log(errores.length);
  //envio a la api los errores
  var email = $('#emailspan').text();
  var nombreuser = $('#nombreuserspan').text();
  
  $.ajax({
    type: "POST",
    url: baseURL + "api/Correo/SendEmailErrors",
    data:{ codPlantilla: 2, usercorreo:email, errores:errores, usernombre:nombreuser},
    beforeSend: function() {    
      swal({
        title: 'Espere..',
        text: 'Enviando correo con los errores por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
        animation: false,
        customClass: 'animated tada',
        allowOutsideClick: false,
        onOpen: () => {
          swal.showLoading()
        }
      }).then((result) => {
          //console.log('cerrado modal')
        
        })
      },
    success: function (response) {
      //console.log(response);
      $.each(response, function (i, v) { 
         
        //cambio el tititulo si es diferente de error
        if(v.type!=="error"){
          titulo="Mensaje";
        }else{
          titulo=v.type;
        }
        swal(
          titulo,
          v.value,
          v.type
        ).then((result) => {
          //console.log('cerrado modal')
            window.location.href = baseURL +"Home";
        })
          
      });
      
    }
  }).fail(function (jqXHR, textStatus, errorThrown) {
    //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
    console.error(textStatus, errorThrown); // Algo fallo
    //notifico al usuario del error al enviar correo y recargo la pagina
    swal({
      title: 'Error',
        text: "Lo sentimos la plataforma no pudo enviar el correo por favor comuníquese con el administrador",
      type: 'error',
      showCancelButton: false,
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Recargar'
    }).then((result) => {
      //if (result.value) {
        location.reload();
      //}
    })
  });
  //limpio la variable errores
  errores.length=0;
}