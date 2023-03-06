framework = {
    redirect: function (rota) {
        var url = 'http://' + window.location.host + '/' + rota;
        window.location.replace(url);
    },

    validarForm: function (seletor) {
        var form = $(seletor);
        $.validator.unobtrusive.parseDynamicContent(seletor);
        return form.valid();
    },

    submeterForm: function (seletor, url, callback) {
        var form = $(seletor);
        var dados = form.serialize();

        if (!url)
            url = form.attr('action');

        $.ajax({
            url: url,
            type: 'POST',
            data: dados,
            error: function () {
                $.notifyError('Ocorreu um erro ao realizar a operação.');
                return;
            }
        }).done(function (response) {
            if (callback)
                callback(response);
        });

        return false;
    },

    carregarGrid: function (tableName, colunas, url, arrayFiltro, fileName) {

        if (typeof (fileName) == 'undefined' || fileName == null) {
            var data = this.getDateTimeNow().toString().replace(/\D/g, "");
            fileName = url.split("/")[1] + "_" + data;
        }

        $('#' + tableName).DataTable({
            //language: { "url": "/js/datatables/Portuguese-Brasil.json" },
            processing: false,
            serverSide: true,
            search: true,
            sDom: "<'row'<'col-md-6'l><'col-md-3 text-right'B><'col-md-3'f>><'row'<'col-md-12'tr>><'row'<'col-md-5'i><'col-md-7'p>>",
            buttons: [{ extend: 'excelHtml5', text: 'Excel', titleAttr: 'Exportação Excel', className: 'button-xls', title: fileName }],
            sPaginationType: "full_numbers",
            iDisplayLength: 10,
            lengthMenu: [[10, 50, 100, -1], [10, 50, 100, 'All']],
            bDestroy: true,
            searchDelay: 100,
            stateSave: false,
            ajax: {
                type: "POST",
                url: url,
                data: function (d) {
                    return { jsonDataTables: JSON.stringify(d), jsonFiltro: JSON.stringify(arrayFiltro) };
                },
                complete: function () {
                    $('#pnlConteudo').removeClass('d-none');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('#pnlConteudo').addClass('d-none');
                    $.notifyError('Ocorreu um erro ao realizar a pesquisa.');
                }
            },
            columns: colunas
        });
    },

    valorValido: function (valor) {
        return (typeof (valor) !== 'undefined' && valor !== null && valor !== '');
    },

    getDateTimeNow: function () {
        var now = new Date();
        var dia = (("0" + now.getDate().toString()).substr(-2));
        var mes = (("0" + (now.getMonth() + 1).toString()).substr(-2));

        return (dia + '/' + mes + '/' + now.getFullYear() + " " + now.getHours() + ':'
            + ((now.getMinutes() < 10) ? ("0" + now.getMinutes()) : (now.getMinutes())) + ':' + ((now.getSeconds() < 10) ? ("0" + now
                .getSeconds()) : (now.getSeconds())));
    },

    abrirModalSmall: function (url, title) {
        this.abrirModal(url, title, 'small');
    },

    abrirModalMedium: function (url, title) {
        this.abrirModal(url, title, 'medium');
    },

    abrirModalLarge: function (url, title) {
        this.abrirModal(url, title, 'large');
    },

    abrirModal: function (url, title, typeSize) {
        $('#_modal_body').load(url, function () {
            this.configurarModal(title, typeSize);
        });
    },

    carregarModalSmall: function (data, title) {
        this.carregarModal(data, title, 'small');
    },

    carregarModalMedium: function (data, title) {
        this.carregarModal(data, title, 'medium');
    },

    carregarModalLarge: function (data, title) {
        this.carregarModal(data, title, 'large');
    },

    carregarModal: function (data, title, typeSize) {
        $('#_modal_body').html(data);
        this.configurarModal(title, typeSize);
        $("#_modal").modal('show');
    },

    configurarModal: function (title, typeSize) {
        $('#_modal_title').text(title);

        $('#_modal_dialog').removeClass('modal-bol-sm');
        $('#_modal_dialog').removeClass('modal-bol-md');
        $('#_modal_dialog').removeClass('modal-bol-lg');

        if (typeSize == 'small')
            $('#_modal_dialog').addClass('modal-bol-sm');
        else if (typeSize == 'medium')
            $('#_modal_dialog').addClass('modal-bol-md');
        else
            $('#_modal_dialog').addClass('modal-bol-lg');

        //$('.seleciona').select2({
        //    allowClear: true,
        //    placeholder: "Selecione",
        //    formatNoMatches: "Nenhum registro encontrado"
        //});
    },

    fecharModal: function (callback) {
        $("#_modal").modal('hide');
        if (callback)
            callback();
    },

    configrarDatePicker: function (element) {
        element.datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'pt-br'
        });
    },

    aplicarMascara: function (sender, mascara) {
        $(document.getElementById(sender.id)).mask(mascara);
    },

    validarNumero: function (e) {
        var evt = (e) ? e : window.event;
        var charCode = (evt.keyCode) ? evt.keyCode : evt.which;

        if ($.inArray(charCode, [8, 9, 13, 27, 46, 110, 188, 190, 194]) === -1) /*Backspace, Tab, Enter, Esc, Delete*/
            return (charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105);

        return true;
    },

    titleCase: function (str) {
        str = str.toLowerCase().split(' ');
        for (var i = 0; i < str.length; i++) {
            str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
        }
        return str.join(' ');
    },

    configurarCalendario: function () {
        $('.datepicker').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'pt-BR',
        });
    },
}

jQuery.notifyDefault = function (msg) {
    swal({
        text: msg,
        buttonsStyling: false,
        confirmButtonClass: "btn"
    }).catch(swal.noop)
};

jQuery.notifyInfo = function (msg) {
    swal({
        text: msg,
        buttonsStyling: false,
        confirmButtonClass: "btn btn-warning botao-filtro"
    }).catch(swal.noop)
};

jQuery.notifySuccess = function (msg) {
    swal({
        text: msg,
        buttonsStyling: false,
        confirmButtonClass: "btn btn-success"
    }).catch(swal.noop)
};

jQuery.notifyError = function (msg) {
    swal({
        text: msg,
        buttonsStyling: false,
        confirmButtonClass: "btn btn-error"
    }).catch(swal.noop)
};

jQuery.notifyWarning = function (msg) {
    swal({
        text: msg,
        buttonsStyling: false,
        confirmButtonClass: "btn btn-warning"
    }).catch(swal.noop)
};