using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DESCARGAR_CATALOGO.Models;

[PrimaryKey("ItemCode", "PriceList")]
[Table("ITM1")]
[Index("Currency", Name = "ITM1_CURRENCY")]
[Index("Ovrwritten", Name = "ITM1_MANUAL")]
[Index("PriceList", Name = "ITM1_PRICE_LIST")]
public partial class Itm1
{
    [Key]
    [StringLength(50)]
    public string ItemCode { get; set; } = null!;

    [Key]
    public short PriceList { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Price { get; set; }

    [StringLength(3)]
    public string? Currency { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Ovrwritten { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Factor { get; set; }

    public int? LogInstanc { get; set; }

    [StringLength(20)]
    public string? ObjType { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AddPrice1 { get; set; }

    [StringLength(3)]
    public string? Currency1 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AddPrice2 { get; set; }

    [StringLength(3)]
    public string? Currency2 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Ovrwrite1 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Ovrwrite2 { get; set; }

    [Column("BasePLNum")]
    public short? BasePlnum { get; set; }

    public int? UomEntry { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PriceType { get; set; }
}
