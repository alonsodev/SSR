/*!
 * FileInput Spanish Translations
 *
 * This file must be loaded after 'fileinput.js'. Patterns in braces '{}', or
 * any HTML markup tags in the messages must not be converted or translated.
 *
 * @see http://github.com/kartik-v/bootstrap-fileinput
 *
 * NOTE: this file must be saved in UTF-8 encoding.
 */
(function ($) {
    "use strict";

    $.fn.fileinputLocales['es'] = {
        fileSingle: 'archivo',
        filePlural: 'archivos',
        browseLabel: 'Examinar &hellip;',
        removeLabel: 'Quitar',
        removeTitle: 'Quitar archivos seleccionados',
        cancelLabel: 'Cancelar',
        cancelTitle: 'Abortar la subida en curso',
        uploadLabel: 'Subir archivo',
        uploadTitle: 'Subir archivos seleccionados',
        msgNo: 'No',
        msgNoFilesSelected: 'No hay archivos seleccionados',
        msgCancelled: 'Cancelado',
        msgPlaceholder: 'Seleccionar {files}...',
        msgZoomModalHeading: 'Vista previa detallada',
        msgFileRequired: 'Debes seleccionar un archivo para subir.',
        msgSizeTooSmall: 'El archivo "{name}" (<b>{size} KB</b>) es demasiado pequeÃ±o y debe ser mayor de <b>{minSize} KB</b>.',
        msgSizeTooLarge: 'El archivo "{name}" (<b>{size} KB</b>) excede el tamaÃ±o mÃ¡ximo permitido de <b>{maxSize} KB</b>.',
        msgFilesTooLess: 'Debe seleccionar al menos <b>{n}</b> {files} a cargar.',
        msgFilesTooMany: 'El nÃºmero de archivos seleccionados a cargar <b>({n})</b> excede el lÃ­mite mÃ¡ximo permitido de <b>{m}</b>.',
        msgFileNotFound: 'Archivo "{name}" no encontrado.',
        msgFileSecured: 'No es posible acceder al archivo "{name}" porque estÃ¡ siendo usado por otra aplicaciÃ³n o no tiene permisos de lectura.',
        msgFileNotReadable: 'No es posible acceder al archivo "{name}".',
        msgFilePreviewAborted: 'PrevisualizaciÃ³n del archivo "{name}" cancelada.',
        msgFilePreviewError: 'OcurriÃ³ un error mientras se leÃ­a el archivo "{name}".',
        msgInvalidFileName: 'Caracteres no vÃ¡lidos o no soportados en el nombre del archivo "{name}".',
        msgInvalidFileType: 'Tipo de archivo no vÃ¡lido para "{name}". SÃ³lo se permiten archivos de tipo "{types}".',
        msgInvalidFileExtension: 'ExtensiÃ³n de archivo no vÃ¡lida para "{name}". SÃ³lo se permiten archivos "{extensions}".',
        msgFileTypes: {
            'image': 'image',
            'html': 'HTML',
            'text': 'text',
            'video': 'video',
            'audio': 'audio',
            'flash': 'flash',
            'pdf': 'PDF',
            'object': 'object'
        },
        msgUploadAborted: 'La carga de archivos se ha cancelado',
        msgUploadThreshold: 'Procesando...',
        msgUploadBegin: 'Inicializando...',
        msgUploadEnd: 'Hecho',
        msgUploadEmpty: 'No existen datos vÃ¡lidos para el envÃ­o.',
        msgUploadError: 'Error',
        msgValidationError: 'Error de validaciÃ³n',
        msgLoading: 'Subiendo archivo {index} de {files} &hellip;',
        msgProgress: 'Subiendo archivo {index} de {files} - {name} - {percent}% completado.',
        msgSelected: '{n} {files} seleccionado(s)',
        msgFoldersNotAllowed: 'Arrastre y suelte Ãºnicamente archivos. Omitida(s) {n} carpeta(s).',
        msgImageWidthSmall: 'El ancho de la imagen "{name}" debe ser de al menos {size} px.',
        msgImageHeightSmall: 'La altura de la imagen "{name}" debe ser de al menos {size} px.',
        msgImageWidthLarge: 'El ancho de la imagen "{name}" no puede exceder de {size} px.',
        msgImageHeightLarge: 'La altura de la imagen "{name}" no puede exceder de {size} px.',
        msgImageResizeError: 'No se pudieron obtener las dimensiones de la imagen para cambiar el tamaÃ±o.',
        msgImageResizeException: 'Error al cambiar el tamaÃ±o de la imagen.<pre>{errors}</pre>',
        msgAjaxError: 'Algo ha ido mal con la operaciÃ³n {operation}. Por favor, intÃ©ntelo de nuevo mas tarde.',
        msgAjaxProgressError: 'La operaciÃ³n {operation} ha fallado',
        ajaxOperations: {
            deleteThumb: 'Archivo borrado',
            uploadThumb: 'Archivo subido',
            uploadBatch: 'Datos subidos en lote',
            uploadExtra: 'Datos del formulario subidos '
        },
        dropZoneTitle: 'Arrastre y suelte aquÃ­ los archivos &hellip;',
        dropZoneClickTitle: '<br>(o haga clic para seleccionar {files})',
        fileActionSettings: {
            removeTitle: 'Eliminar archivo',
            uploadTitle: 'Subir archivo',
            uploadRetryTitle: 'Reintentar subir',
            downloadTitle: 'Descargar archivo',
            zoomTitle: 'Ver detalles',
            dragTitle: 'Mover / Reordenar',
            indicatorNewTitle: 'No subido todavÃ­a',
            indicatorSuccessTitle: 'Subido',
            indicatorErrorTitle: 'Error al subir',
            indicatorLoadingTitle: 'Subiendo...'
        },
        previewZoomButtonTitles: {
            prev: 'Anterior',
            next: 'Siguiente',
            toggleheader: 'Mostrar encabezado',
            fullscreen: 'Pantalla completa',
            borderless: 'Modo sin bordes',
            close: 'Cerrar vista detallada'
        }
    };
})(window.jQuery);
