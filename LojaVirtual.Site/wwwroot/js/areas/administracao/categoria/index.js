$(function () {
    ativarBotoes();

    $('.btn-pesquisar').on('click', function (e) {
        e.preventDefault();
        obterPaginado(1);
    });

    $('.qtdPorPagina').on('change', function () {
        let qtdInput = $(this);
        let qtd = parseInt(qtdInput.val());
        if (isNaN(qtd)) {
            qtd = 10;
        }
        else if (qtd < 1) {
            qtd = 1;
        }
        else if (qtd > 100) {
            qtd = 100;
        }
        qtdInput.val(qtd);
        obterPaginado(1);
    });

    $('.ordem').on('change', function () {
        obterPaginado(1);
    });

    $('.desc').on('change', function () {
        obterPaginado(1);
    });

    function obterPaginado(pagina) {
        $.get('categorias/paginacao'
            + '?pagina=' + pagina
            + '&qtdPorPagina=' + $('.qtdPorPagina').val()
            + '&ordem=' + $('.ordem').val()
            + '&desc=' + $('.desc').is(':checked')
            + '&pesquisa=' + $('.pesquisa').val())
            .done(function (response) {
                $(".paginacao").html(response);
                ativarBotoes();
            });
    }

    function ativarBotoes() {
        $('.pagina').on('click', function () {
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado($(this).text());
        });

        $('.paginaAtual').on('click', function () {
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
        });

        $('.paginaAnterior').on('click', function () {
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) - 1);
        });

        $('.paginaProxima').on('click', function () {
            $('html, body').animate({ scrollTop: $(".paginacao").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) + 1);
        });
    }
});