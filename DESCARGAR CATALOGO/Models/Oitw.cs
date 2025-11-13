using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DESCARGAR_CATALOGO.Models;

[PrimaryKey("ItemCode", "WhsCode")]
[Table("OITW")]
[Index("DftBinAbs", Name = "OITW_DFT_BIN")]
[Index("WhsCode", Name = "OITW_WHS")]
public partial class Oitw
{
    [Key]
    [StringLength(50)]
    public string ItemCode { get; set; } = null!;

    [Key]
    [StringLength(8)]
    public string WhsCode { get; set; } = null!;

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OnHand { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? IsCommited { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OnOrder { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Consig { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Counted { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? WasCounted { get; set; }

    public short? UserSign { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MinStock { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MaxStock { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MinOrder { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AvgPrice { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Locked { get; set; }

    [StringLength(15)]
    public string? BalInvntAc { get; set; }

    [StringLength(15)]
    public string? SaleCostAc { get; set; }

    [StringLength(15)]
    public string? TransferAc { get; set; }

    [StringLength(15)]
    public string? RevenuesAc { get; set; }

    [StringLength(15)]
    public string? VarianceAc { get; set; }

    [StringLength(15)]
    public string? DecreasAc { get; set; }

    [StringLength(15)]
    public string? IncreasAc { get; set; }

    [StringLength(15)]
    public string? ReturnAc { get; set; }

    [StringLength(15)]
    public string? ExpensesAc { get; set; }

    [Column("EURevenuAc")]
    [StringLength(15)]
    public string? EurevenuAc { get; set; }

    [Column("EUExpensAc")]
    [StringLength(15)]
    public string? EuexpensAc { get; set; }

    [StringLength(15)]
    public string? FrRevenuAc { get; set; }

    [StringLength(15)]
    public string? FrExpensAc { get; set; }

    [StringLength(15)]
    public string? ExmptIncom { get; set; }

    [StringLength(15)]
    public string? PriceDifAc { get; set; }

    [StringLength(15)]
    public string? ExchangeAc { get; set; }

    [StringLength(15)]
    public string? BalanceAcc { get; set; }

    [StringLength(15)]
    public string? PurchaseAc { get; set; }

    [Column("PAReturnAc")]
    [StringLength(15)]
    public string? PareturnAc { get; set; }

    [StringLength(15)]
    public string? PurchOfsAc { get; set; }

    [StringLength(15)]
    public string? ShpdGdsAct { get; set; }

    [StringLength(15)]
    public string? VatRevAct { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? StockValue { get; set; }

    [StringLength(15)]
    public string? DecresGlAc { get; set; }

    [StringLength(15)]
    public string? IncresGlAc { get; set; }

    [StringLength(15)]
    public string? StokRvlAct { get; set; }

    [StringLength(15)]
    public string? StkOffsAct { get; set; }

    [StringLength(15)]
    public string? WipAcct { get; set; }

    [StringLength(15)]
    public string? WipVarAcct { get; set; }

    [StringLength(15)]
    public string? CostRvlAct { get; set; }

    [StringLength(15)]
    public string? CstOffsAct { get; set; }

    [StringLength(15)]
    public string? ExpClrAct { get; set; }

    [StringLength(15)]
    public string? ExpOfstAct { get; set; }

    [StringLength(20)]
    public string? Object { get; set; }

    [Column("logInstanc")]
    public int? LogInstanc { get; set; }

    [Column("createDate", TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column("userSign2")]
    public short? UserSign2 { get; set; }

    [Column("updateDate", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("ARCMAct")]
    [StringLength(15)]
    public string? Arcmact { get; set; }

    [Column("ARCMFrnAct")]
    [StringLength(15)]
    public string? ArcmfrnAct { get; set; }

    [Column("ARCMEUAct")]
    [StringLength(15)]
    public string? Arcmeuact { get; set; }

    [Column("ARCMExpAct")]
    [StringLength(15)]
    public string? ArcmexpAct { get; set; }

    [Column("APCMAct")]
    [StringLength(15)]
    public string? Apcmact { get; set; }

    [Column("APCMFrnAct")]
    [StringLength(15)]
    public string? ApcmfrnAct { get; set; }

    [Column("APCMEUAct")]
    [StringLength(15)]
    public string? Apcmeuact { get; set; }

    [StringLength(15)]
    public string? RevRetAct { get; set; }

    [StringLength(15)]
    public string? NegStckAct { get; set; }

    [StringLength(15)]
    public string? StkInTnAct { get; set; }

    [StringLength(15)]
    public string? PurBalAct { get; set; }

    [Column("WhICenAct")]
    [StringLength(15)]
    public string? WhIcenAct { get; set; }

    [Column("WhOCenAct")]
    [StringLength(15)]
    public string? WhOcenAct { get; set; }

    [StringLength(15)]
    public string? WipOffset { get; set; }

    [StringLength(15)]
    public string? StockOffst { get; set; }

    public int? DftBinAbs { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? DftBinEnfd { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Freezed { get; set; }

    public int? FreezeDoc { get; set; }

    [Column("FreeChrgSA")]
    [StringLength(15)]
    public string? FreeChrgSa { get; set; }

    [Column("FreeChrgPU")]
    [StringLength(15)]
    public string? FreeChrgPu { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? IndEscala { get; set; }

    [Column("CNJPMan")]
    [StringLength(14)]
    public string? Cnjpman { get; set; }

    [Column("U_Lock_fg")]
    public short? ULockFg { get; set; }
}
