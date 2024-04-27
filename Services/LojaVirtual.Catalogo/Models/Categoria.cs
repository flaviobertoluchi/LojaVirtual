﻿using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Produtos.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
    }
}
