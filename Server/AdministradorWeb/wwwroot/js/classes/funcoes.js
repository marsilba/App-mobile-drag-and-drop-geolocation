carregarTipoOcorrencia = function (_select) {
    var items = [];
    items.push('<option value="" selected="selected">Selecione</option>');
    items.push('<option value="1">Atendimento</option>');
    items.push('<option value="2">CMA/GCM</option>');
    items.push('<option value="3">Criminal</option>');
    items.push('<option value="4">Ordem Pública</option>');
    items.push('<option value="5">Trânsito</option>');
    items.push('<option value="6">Outros</option>');

    $(document.getElementById(_select)).html(items);
};

carregarSubTipoOcorrencia = function (_selectOrigem, _select) {
    var idTipoOcorrencia = $(document.getElementById(_selectOrigem)).val();

    $(document.getElementById(_select)).empty();

    var items = [];
    items.push('<option value="" selected="selected">Selecione</option>');

    if (idTipoOcorrencia == '1') {
        items.push('<option value="1">192 SAMU</option>');
        items.push('<option value="2">193 Bombeiro Combatente</option>');
        items.push('<option value="3">193 Bombeiro Socorrista</option>');
        items.push('<option value="4">199 Defesa Civil</option>');
        items.push('<option value="5">Outros</option>');
    }
    else if (idTipoOcorrencia == '2') {
        items.push('<option value="1">Combate a Incêncio</option>');
        items.push('<option value="2">Fiscalização Ambiental</option>');
        items.push('<option value="3">Resgate de Animais</option>');
        items.push('<option value="4">Outros</option>');
    }
    else if (idTipoOcorrencia == '3') {
        items.push('<option value="1">Ameaça</option>');
        items.push('<option value="2">Cercamento</option>');
        items.push('<option value="3">Conduta Incoveniente</option>');
        items.push('<option value="4">Furto</option>');
        items.push('<option value="5">Homicídio</option>');
        items.push('<option value="6">Lesão Corporal</option>');
        items.push('<option value="7">Roubo</option>');
        items.push('<option value="8">Outros</option>');
    }
    else if (idTipoOcorrencia == '4') {
        items.push('<option value="1">Apoio Social</option>');
        items.push('<option value="2">Controle Urbano</option>');
        items.push('<option value="3">Posturas</option>');
        items.push('<option value="4">Outros</option>');
    }
    else if (idTipoOcorrencia == '5') {
        items.push('<option value="1">Acidente com Vítima</option>');
        items.push('<option value="2">Acidente sem Vítima</option>');
        items.push('<option value="3">Fiscalização</option>');
        items.push('<option value="4">Semafórica</option>');
        items.push('<option value="5">Outros</option>');
    }
    else if (idTipoOcorrencia == '6') {
        items.push('<option value="1">Elogios</option>');
        items.push('<option value="2">Críticas</option>');
        items.push('<option value="3">Sugestões</option>');
        items.push('<option value="4">Suporte / Chamados</option>');
        items.push('<option value="5">Outros</option>');
    }

    $(document.getElementById(_select)).html(items);
};