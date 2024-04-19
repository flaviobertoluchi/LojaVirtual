$(function () {
    $('.btn-pesquisar').on('click', function (e) {
        e.preventDefault();
        obterPaginado();
    });

    $('.qtdPorPagina').on('change', function () {
        obterPaginado();
    });

    $('.ordem').on('change', function () {
        obterPaginado();
    });

    function obterPaginado() {
        $.get('/Produto/CatalogoProdutos/'
            + '?pagina=' + 1
            + '&qtdPorPagina=' + $('.qtdPorPagina').val()
            + '&pesquisa=' + $('.pesquisaProdutos').val()
            + '&ordem=' + $('.ordem').val())
            .done(function (response) {
                $(".catalogo-produtos").html(response);
            });
    }
});