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
            setTimeout(() => {
                atualizarCarrinhoMenu();
                atualizarCarrinho();
            }, 1000)
        );
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

    function atualizarCarrinho() {
        $.get('carrinho/carrinhopartial')
            .done(function (response) {
                $('.carrinhopartial').html(response);
                ativarBotoes();
            })
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

            $.ajax(
                {
                    url: 'carrinho/' + $(this).parent().find('.produtoId').val(),
                    method: 'DELETE',
                    headers: {
                        RequestVerificationToken: $('#RequestVerificationToken').val()
                    }
                }
            ).done(
                $(this).parent().parent().parent().html(''),
                setTimeout(() => {
                    atualizarCarrinhoMenu();
                    atualizarCarrinho();
                }, 1000)
            );
        });
    }
});
