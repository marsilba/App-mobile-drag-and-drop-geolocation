notificacao = {
    verificar: function () {
        $.ajax({
            global: false,
            type: 'GET',
            url: '/notificacoespendentes/verificarpendencias',
            success: function (response) {
                $('#qtdNotificacoes').html('');
                $('#spnDetalhesNotificacoes').html('');
                $('#msgNotificacao').html('');

                if (!response.success) {
                    $('#qtdNotificacoes').addClass('d-none');
                    $('#msgNotificacao').html('Nenhuma notificação');
                    return;
                }

                //#########################################################
                //clearInterval(setIntervalNotificacao);

                var htmlNotification = '';
                var mensagem = '';

                for (var i = 0; i < response.model.length; i++) {

                    if (i < 5) {
                        mensagem = 'Processo:  ' + response.model[i].numeroProcesso +
                            '<br>Tipo Solicitação:  ' + response.model[i].tipoSolicitacao +
                            '<br>Data Abertura:  ' + response.model[i].dataAbertura;

                        htmlNotification += '<a class="dropdown-item" href="javascript:notificacao.detalharNotificacao(' + response.model[i].id + ');">' + mensagem + '</a>';
                    }


                    if (response.model.length > 5)
                        $('#msgNotificacao').html('Últimas 5 notificações');

                    if (response.model[i].flagVisualizado == false)
                        notificacao.showNotification(response.model[i]);
                }

                $('#qtdNotificacoes').html(response.model.length);
                $('#spnDetalhesNotificacoes').html(htmlNotification);

                $('#qtdNotificacoes').removeClass('d-none');
            }
        });
    },

    showNotification: function (ambulante) {
        //type = ['', 'info', 'success', 'warning', 'danger', 'rose', 'primary'];
        $.notify({
            icon: "notifications",
            title: 'Solicitação: ' + ambulante.tipoSolicitacao,
            message: 'Processo: ' + ambulante.numeroProcesso + '<br>Solicitante: ' + ambulante.nome,
        }, {
                type: 'danger',
                timer: 15000,
                placement: {
                    from: 'top',
                    align: 'right'
                }
            });
    },

    detalharNotificacao: function (_id) {

        $.ajax({
            type: "POST",
            url: "/NotificacoesPendentes/ObterSolicitacao",
            data: { id: _id },
            success: function (response) {
                $('#_modal_body_liberacao').html(response);
                $('#_modal_dialog_liberacao').addClass('modal-bol-lg');
                $("#_modal_liberacao").modal('show');
            }
        });
    }
}