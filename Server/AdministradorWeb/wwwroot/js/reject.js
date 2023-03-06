function reject() {
    $.reject({
        reject: { msie5: true, msie6: true, msie7: true, msie8: true },
        display: ['chrome', 'firefox', 'msie'],
        close: false,
        header: "Navegador não suportado!",
        paragraph1: "O Sistema não é compatível com a versão do navegador que você está utilizando.",
        paragraph2: "Navegadores compatíveis (versões a partir de):",
        browserInfo: { chrome: { text: "Google Chrome 28" }, firefox: { text: "Mozilla Firefox 22" }, msie: { text: "Internet Explorer 10" } }
    });
};