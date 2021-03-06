﻿/**
* @author ALEXANDER CIFUENTES SANCHEZ
* @description este archivo en ralidad no lee los archivos los carga para que sean mas rapido a la hora 
+ de leer por el codigo
+ El archivo lector es validaCargaArchivo.js
*/
var dropZoneId = "drop-zone";
var buttonId = "clickHere";
var mouseOverClass = "mouse-over";
var dropZone = $("#" + dropZoneId);
var inputFile = dropZone.find("input");
var finalFiles = [];

$(document).ready(function () {
    
  
  var ooleft = dropZone.offset().left;
  var ooright = dropZone.outerWidth() + ooleft;
  var ootop = dropZone.offset().top;
  var oobottom = dropZone.outerHeight() + ootop;

  document.getElementById(dropZoneId).addEventListener("dragover", function (e) {
      e.preventDefault();
      e.stopPropagation();
      dropZone.addClass(mouseOverClass);
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

      $("#" + buttonId).mousemove(function (e) {
          var x = e.pageX;
          var y = e.pageY;
          if (!(x < oleft || x > oright || y < otop || y > obottom)) {
              inputFile.offset({
                  top: y - 15,
                  left: x - 160
              });
          } else {
              inputFile.offset({
                  top: -400,
                  left: -400
              });
          }
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
          //finalFiles[idx].push(elm);
      });

      for (initial; initial < fileNum; initial++) {
          counter = counter + 1;
          $('#filename').append('<div id="file_' + initial + '"><span class="fa-stack fa-lg"><i class="fa fa-file fa-stack-1x "></i><strong class="fa-stack-1x" style="color:#FFF; font-size:12px; margin-top:2px;">' + counter + '</strong></span> ' + this.files[initial].name + '&nbsp;&nbsp;<span class="fa fa-times-circle fa-lg closeBtn" onclick="limpiarCampos()" title="remove"></span></div>');
      }
  });



})

function removeLine(obj) {
  
  inputFile.val('');
  var jqObj = $(obj);
  var container = jqObj.closest('div');
  var index = container.attr("id").split('_')[1];
  container.remove();

  delete finalFiles[index];
  inputFile.val(finalFiles);
  //console.log(finalFiles);
}
 