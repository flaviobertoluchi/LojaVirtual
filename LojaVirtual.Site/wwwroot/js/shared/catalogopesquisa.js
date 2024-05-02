$(function () {
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

    $('.categoria').on('change', function () {
        obterPaginado(1);
    });

    function obterPaginado(pagina) {
        $.get('produtos/catalogo'
            + '?pagina=' + pagina
            + '&qtdPorPagina=' + $('.qtdPorPagina').val()
            + '&pesquisa=' + $('.pesquisaProdutos').val()
            + '&ordem=' + $('.ordem').val()
            + '&categoriaId=' + $('.categoria').val())
            .done(function (response) {
                $(".catalogo-produtos").html(response);
                paginacao();
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

        $('.card').on(
            {
                mouseenter: function () {
                    $(this).addClass("shadow");
                },
                mouseleave: function () {
                    $(this).removeClass("shadow");
                }
            });

        $('.carrinhoDireto').on('click', function () {
            $(this).addClass('fa-beat-fade');

            $.post(
                {
                    url: 'carrinho',
                    headers: {
                        RequestVerificationToken: $('#RequestVerificationToken').val()
                    },
                    data: JSON.stringify(
                        {
                            produtoid: parseInt($(this).find('.produtoId').val()),
                            quantidade: 1
                        }),
                    contentType: 'application/json'
                }
            ).done(
                setTimeout(() => {
                    atualizarCarrinhoMenu()
                }, 1000)
            );

            setTimeout(() => {
                $(this).removeClass('fa-beat-fade');
            }, 1000);
        });
    }

    function atualizarCarrinhoMenu() {
        let carrinhoMenu = $(".carrinhoMenu");

        carrinhoMenu.addClass('fa-beat-fade');

        $.get('carrinho/carrinhomenu')
            .done(function (response) {
                carrinhoMenu.html(response);
            })

        setTimeout(() => {
            carrinhoMenu.removeClass('fa-beat-fade');
        }, 1000);
    }
});
