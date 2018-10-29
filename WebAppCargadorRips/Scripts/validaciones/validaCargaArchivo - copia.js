$(document).ready(function () {
    $('select').material_select();
    
    // Unhide vanilla select: https://github.com/Dogfalo/materialize/issues/1861
    $('select[required]').css({
          display: 'inline',
          position: 'absolute',
          float: 'left',
          padding: 0,
          margin: 0,
          border: '1px solid rgba(255,255,255,0)',
          height: 0, 
          width: 0,
          top: '2em',
          left: '3em',
          //opacity: 0,
          //pointerEvents: 'none'
        });
    
    // Extension pour comptabilité avec materialize.css
    $.validator.setDefaults({
      highlight: function(element, errorClass, validClass) {
        console.log(element.tagName);
        if (element.tagName === 'SELECT')
            $(element).closest('.select-wrapper').addClass('invalid');
        else
            $(element).removeClass(errorClass).addClass(validClass);
      },
      unhighlight: function(element, errorClass, validClass) {
        if (element.tagName === 'SELECT')
            $(element).closest('.select-wrapper').removeClass('invalid');
        else
            $(element).removeClass(errorClass).addClass(validClass);
      },
      errorClass: 'invalid',
      validClass: "valid",
      errorPlacement: function(error, element) {
        if (element.prop('tagName')  === 'SELECT') {
          // alternate placement for select error
          error.appendTo( element.parent() );
          error.addClass('active');
        }
        else {
          $(element)
            .closest("form")
            .find("label[for='" + element.attr("id") + "']")
            .attr('data-error', error.text());
        }
      },
      
    });

$("#formulariocargaarchivo").submit(function(e) {
  e.preventDefault();
}).validate({
  //debug: true,
    rules: {
        //tipoUsuario:"required",
        categoria:"required",
        //fechaInicio:"required",
        fechaFin: {
            required: true,
        },
        rips: {
          required: true,
      },
    },
    //For custom messages
    messages: {
        tipoUsuario:"Seleccione una opción",
        categoria:"Seleccione una categoria",
        fechaInicio: "Indique una fecha de inicio",
        fechaFin: "Indique una fecha de fin",
        rips: "Seleccione un archivo",
    },
    errorElement : 'div',
    errorPlacement: function(error, element) {
      var placement = $(element).data('error');
      if (placement) {
        $(placement).append(error)
      } else {
        error.insertAfter(element);
        //event.preventDefault();
      }
    },
   
    submitHandler: function(e) {
      
        //e.preventDefault();
         //console.log(" formulario envia submit");
         //console.log(e);
         readFile();
         //$(form).ajaxSubmit();
         //debugger; onsubmit: true,
         
        }
 });

 
})

function readFile() {
  let bandera=true;//variable buleana bandera que me permitira controlar las validaciones
  var fileInput = document.getElementById('archivos');
  console.log(fileInput);
  console.log(finalFiles);
  var fileDisplayArea = document.getElementById('fileDisplayArea');
  var cantidad = fileInput.length;
  console.log(finalFiles.length);
  
  //almaceno el nombre de los archivos en un array para su posterior validacion, ya que no se pueden enviar repetidos
  

  for (let i = 0; i < cantidad; i++) {
    nombre.push((fileInput.file[i]['name']).substring(2, 0));
  }
  
  //Valido que el usuario no seleccione archivos iguales
  //aplico un ordenamiento burbuja para validar que no existan archivos repetidos
  for (let i = 0; i<cantidad-1; i++){
    for (let j =i+1; j<cantidad; j++){

    if (nombre[i] == nombre[j])
    {
      //Cambio el balor de la variable de validación
      bandera=false;
    }
  }
 }
  
 //valido que la variable de validacion no cambio
  if (bandera===true) {
    console.log(cantidad);
    // si la variable bandera no cambio envio lectura de archivos
    for (let i = 0; i < cantidad; i++) {
      var file = fileInput.files[i];
      var textType = /text.*/;
      

      if (file.type.match(textType)) {
        var reader = new FileReader();
        reader.onload = function(e) {
          fileDisplayArea.innerText = reader.result;
          // By lines
            var lines = this.result.split('\n');
            //console.log(lines);
            /*for(var line = 0; line < lines.length; line++){
              console.log(lines[line]);
            }*/
        }
        reader.readAsText(file);	
      } else {
        fileDisplayArea.innerText = "Archivos No Soportados!"
      }
    }//fin for
  } else {
    //si cambio la variable bandera
    //evito la carga innesaria de los archivos
    //le indico al usuario que por favor revice la info a cargar
    console.log("valide los archivos a cargar eh intente nuevamente");
    //limpiarCampos();
  }
  
}
 