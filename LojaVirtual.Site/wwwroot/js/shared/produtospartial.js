$(function () {
    shadowCards();
    paginacao();

    $('.btn-pesquisar').on('click', function (e) {
        e.preventDefault();
        obterPaginado(1);
    });

    $('.qtdPorPagina').on('change', function () {
        obterPaginado(1);
    });

    $('.ordem').on('change', function () {
        obterPaginado(1);
    });

    function obterPaginado(pagina) {
        $.get('/Produto/CatalogoProdutos/'
            + '?pagina=' + pagina
            + '&qtdPorPagina=' + $('.qtdPorPagina').val()
            + '&pesquisa=' + $('.pesquisaProdutos').val()
            + '&ordem=' + $('.ordem').val())
            .done(function (response) {
                $(".catalogo-produtos").html(response);
                shadowCards();
                paginacao();
            });
    }

    function shadowCards() {
        $('.card').on({
            mouseenter: function () {
                $(this).addClass("shadow");
            },
            mouseleave: function () {
                $(this).removeClass("shadow");
            }
        });
    }

    function paginacao() {
        $('.pagina').on('click', function () {
            $('html, body').animate({ scrollTop: $("#produtos").position().top }, 1);
            obterPaginado($(this).text());
        });

        $('.paginaAtual').on('click', function () {
            $('html, body').animate({ scrollTop: $("#produtos").position().top }, 1);
        });

        $('.paginaAnterior').on('click', function () {
            $('html, body').animate({ scrollTop: $("#produtos").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) - 1);
        });

        $('.paginaProxima').on('click', function () {
            $('html, body').animate({ scrollTop: $("#produtos").position().top }, 1);
            obterPaginado(parseInt($('.paginaAtual').text()) + 1);
        });
    }
});