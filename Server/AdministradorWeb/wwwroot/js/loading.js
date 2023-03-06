$(document).ready(function () {

    //blockUI defaults
    $.blockUI.defaults.message = '<img src="/images/loading.gif" />';
    $.blockUI.defaults.css.border = 'none';
    $.blockUI.defaults.css.backgroundColor = 'transparent';
    $.blockUI.defaults.css.opacity = 0.6;
    $.blockUI.defaults.overlayCSS.backgroundColor = '#fff';
    $.blockUI.defaults.overlayCSS.opacity = 0.6;
    $.blockUI.defaults.baseZ = 10000;

    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);    
});