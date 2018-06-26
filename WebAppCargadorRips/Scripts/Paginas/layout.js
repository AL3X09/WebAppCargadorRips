/**
 * Created by Alex on 11/03/2017.
 */
const getUrl = window.location;
const baseURL = getUrl.protocol + "//" + getUrl.host + "/"; // lineas servidor local
//const baseURL = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1] + "/"; // lineas servidor local
$(document).ready(function () {
  // Initialize collapse button del menu
  $("#button-collapse").sideNav();
  
});

function gettALLME() {
  
}

function vistaDescargarValidador() {
  window.open('https://docs.google.com/forms/d/e/1FAIpQLSdZOk-bHZ8lhyZ_s5iqbznbGUYgXoYny3kDhqHmkSQHRyYyIw/viewform?embedded=true');
}

/**
 * FUNCION NO USUADA ELIMINAR
 
function inicilizoNAVS() {
  //cargo tabs en el nav de la aplicación para darle acceso a facil al usuario
  $('#tabsNav').append('<ul class="tabs tabs-transparent" id="tabsNav">'+
  '<li class="tab">' +
  '<a class="active tooltipped" data-position="bottom" data-delay="50" data-tooltip="Ver estado de tus RIPS!!" href="#divtabEstado">Estado RIPS</a>' +
  '</li>' +
  '<li class="tab">' +
  '<a href="#divtabCargaRips" class="tooltipped" data-position="bottom" data-delay="50" data-tooltip="Cargar tus RIPS!! desde aquí">Cargar RIPS</a>' +
  '</ul></li>');
}
*/
