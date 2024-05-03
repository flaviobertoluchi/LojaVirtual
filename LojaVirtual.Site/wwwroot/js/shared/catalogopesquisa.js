$(function () {
    ativarBotoes();

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
                ativarBotoes();
            });
    }

    function ativarBotoes() {
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

        $('.card').on({
            mouseenter: function () {
                $(this).addClass("shadow");
            },
            mouseleave: function () {
                $(this).removeClass("shadow");
            }
        });

        $('.carrinhoDireto').on('click', function () {
            let carrinhoDireto = $(this);
            carrinhoDireto.addClass('fa-beat-fade');
            let cookieAntigo = obterCookie('carrinho');

            $.post({
                url: 'carrinho',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: JSON.stringify({
                    produtoid: parseInt(carrinhoDireto.find('.produtoId').val()),
                    quantidade: 1
                }),
                contentType: 'application/json'
            }).done(
                carrinhoDireto.popover('show'),
                setTimeout(function () {
                    carrinhoDireto.popover('hide')
                }, 1500),
                esperaAtualizacaoCookie('carrinho', cookieAntigo).then(function () {
                    atualizarCarrinhoMenu()
                })
            );

            setTimeout(() => {
                carrinhoDireto.removeClass('fa-beat-fade');
            }, 1000);
        });
    }

    function atualizarCarrinhoMenu() {
        let carrinhoMenu = $(".carrinhoMenu");
        carrinhoMenu.addClass('fa-beat-fade');

        $.get('carrinho/carrinhomenu').done(function (response) {
            carrinhoMenu.html(response);
        });

        setTimeout(() => {
            carrinhoMenu.removeClass('fa-beat-fade');
        }, 1000);
    }

    function obterCookie(key) {
        let keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
        return keyValue ? keyValue[2] : null;
    }

    function esperaAtualizacaoCookie(key, cookieAntigo) {
        return new Promise(function (resolve) {
            let intervalId = setInterval(function () {
                let cookie = obterCookie(key);
                if (cookie !== cookieAntigo) {
                    clearInterval(intervalId);
                    resolve();
                }
            }, 100);
        });
    }
});
