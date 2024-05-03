$(function () {
    ativarBotoes();

    function alterarQtd(qtdInput, qtd) {
        let estoque = parseInt(qtdInput.parent().find('.estoque').val());
        if (isNaN(qtd)) {
            qtd = 1;
        }
        else if (qtd < 1) {
            qtd = 1;
        }
        else if (qtd > estoque) {
            qtd = estoque;
        }

        qtdInput.val(qtd);

        let produtoId = qtdInput.parent().find('.produtoId').val();
        let cookieAntigo = obterCookie('carrinho');

        $.ajax(
            {
                url: 'carrinho/' + produtoId,
                method: 'PUT',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: JSON.stringify(
                    {
                        produtoid: parseInt(produtoId),
                        quantidade: parseInt(qtd)
                    }),
                contentType: 'application/json'
            }
        ).done(
            esperaAtualizacaoCookie('carrinho', cookieAntigo).then(function () {
                atualizarCarrinho(); 
            })
        );
    }
     
    function atualizarCarrinho() {
        $.get('carrinho/carrinhopartial')
            .done(function (response) {
                $('.carrinhopartial').html(response);
                ativarBotoes();
            })

        $.get('carrinho/carrinhomenu')
            .done(function (response) {
                $(".carrinhoMenu").html(response);
            });
    }

    function ativarBotoes() {
        $('.qtdMenos').on('click', function (e) {
            e.preventDefault();
            let qtdInput = $(this).parent().find('.qtd');
            alterarQtd(qtdInput, parseInt(qtdInput.val()) - 1);
        });

        $('.qtdMais').on('click', function (e) {
            e.preventDefault();
            let qtdInput = $(this).parent().find('.qtd');
            alterarQtd(qtdInput, parseInt(qtdInput.val()) + 1);
        });

        $('.qtd').on('keypress', function (e) {
            if (e.which == '13') {
                e.preventDefault();
                let qtdInput = $(this).parent().find('.qtd');
                alterarQtd(qtdInput, parseInt(qtdInput.val()));
            }
        });

        $('.qtd').on('change', function () {
            let qtdInput = $(this).parent().find('.qtd');
            alterarQtd(qtdInput, parseInt(qtdInput.val()));
        });

        $('.remover').on('click', function () {
            let cookieAntigo = obterCookie('carrinho');

            $.ajax(
                {
                    url: 'carrinho/' + $(this).parent().find('.produtoId').val(),
                    method: 'DELETE',
                    headers: {
                        RequestVerificationToken: $('#RequestVerificationToken').val()
                    }
                }
            ).done(
                esperaAtualizacaoCookie('carrinho', cookieAntigo).then(function () {
                    atualizarCarrinho(); 
                })
            );
        });
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
