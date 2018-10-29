$(document).ready(function () {
  $('#formulariocargaarchivo').attrvalidate({
    showFieldIndicator: false
  });
    $('select').material_select();
 
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
 