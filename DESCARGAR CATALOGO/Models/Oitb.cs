using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DESCARGAR_CATALOGO.Models;

[Table("OITB")]
[Index("ItmsGrpNam", Name = "OITB_GROUP_NAME", IsUnique = true)]
public partial class Oitb
{
    [Key]
    public short ItmsGrpCod { get; set; }

    [StringLength(100)]
    public string ItmsGrpNam { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string? Locked { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? DataSource { get; set; }

    public short? UserSign { get; set; }

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

    public short? CycleCode { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Alert { get; set; }

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

    [StringLength(15)]
    public string? DecresGlAc { get; set; }

    [StringLength(15)]
    public string? IncresGlAc { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? InvntSys { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PlaningSys { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PrcrmntMtd { get; set; }

    public short? OrdrIntrvl { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OrdrMulti { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MinOrdrQty { get; set; }

    public int? LeadTime { get; set; }

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

    [StringLength(1)]
    [Unicode(false)]
    public string? ItemClass { get; set; }

    [Column("OSvcCode")]
    public int? OsvcCode { get; set; }

    [Column("ISvcCode")]
    public int? IsvcCode { get; set; }

    public int? ServiceGrp { get; set; }

    [Column("NCMCode")]
    public int? Ncmcode { get; set; }

    [StringLength(3)]
    public string? MatType { get; set; }

    public int? MatGrp { get; set; }

    [StringLength(2)]
    public string? ProductSrc { get; set; }

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

    public int? UgpEntry { get; set; }

    [Column("IUoMEntry")]
    public int? IuoMentry { get; set; }

    public int? ToleranDay { get; set; }

    [StringLength(2)]
    public string? RuleCode { get; set; }

    [Column("CompoWH")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CompoWh { get; set; }

    [Column("FreeChrgSA")]
    [StringLength(15)]
    public string? FreeChrgSa { get; set; }

    [Column("FreeChrgPU")]
    [StringLength(15)]
    public string? FreeChrgPu { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? RawMtrl { get; set; }
}
