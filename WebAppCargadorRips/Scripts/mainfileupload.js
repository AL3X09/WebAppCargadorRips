/*
 * jQuery File Upload Plugin JS Example
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * https://opensource.org/licenses/MIT
 */

/* global $, window */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#formulariocargaarchivo').fileupload({
        url: '',
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        //disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
        //maxFileSize: 999000,
        acceptFileTypes: /(\.|\/)(txt)$/i,
        messages: {
            maxNumberOfFiles: 'yourText',   
            acceptFileTypes: 'yourText' ,   
            maxFileSize:  'yourText',
            minFileSize:  'yourText'
         }
    });

   
    if (window.location.hostname === 'blueimp.github.io') {
        // Demo settings:
        $('#formulariocargaarchivo').fileupload('option', {
            url: '',
            // Enable image resizing, except for Android and Opera,
            // which actually support image resizing, but fail to
            // send Blob objects via XHR requests:
            //disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
            maxFileSize: 999000,
            acceptFileTypes: /(\.|\/)(txt)$/i
        });
        // Upload server status check for browsers with CORS support:
        if ($.support.cors) {
            $.ajax({
                url: '',
                type: 'HEAD'
            }).fail(function () {
                $('<div class="alert alert-danger"/>')
                    .text('Upload server currently unavailable - ' +
                            new Date())
                    .appendTo('#formulariocargaarchivo');
            });
        }
    } else {
        // Load existing files:
        $('#formulariocargaarchivo').addClass('fileupload-processing');
        $.ajax({
            // Uncomment the following to send cross-domain cookies:
            //xhrFields: {withCredentials: true},
            url: $('#formulariocargaarchivo').fileupload('option', 'url'),
            dataType: 'json',
            context: $('#formulariocargaarchivo')[0]
        }).always(function () {
            $(this).removeClass('fileupload-processing');
        }).done(function (result) {
            $(this).fileupload('option', 'done')
                .call(this, $.Event('done'), {result: result});
        });
    }

});
