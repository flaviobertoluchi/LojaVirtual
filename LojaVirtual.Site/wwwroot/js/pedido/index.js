$(function () {
    ativarBotoes();

    function obterPaginado(pagina) {
        $.get('pedidos/paginacao?pagina=' + pagina)
            .done(function (response) {
                $(".paginacao").html(response);
                ativarBotoes();
            })
            .fail(function (response) {
                console.log(response);
            });
    }

    function ativarBotoes() {
        $('.pagina').on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado($(this).text());
        });

        $('.paginaAtual').on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
        });

        $('.paginaAnterior').on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) - 1);
        });

        $('.paginaProxima').on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) + 1);
        });
    }
});
