using System;
using Gtk;
using PointOfSale.Domain;

public partial class MainWindow : Gtk.Window, Display
{
	private readonly PointOfSaleService service;
	private readonly ReceiptFactory factory;


	public MainWindow(PointOfSaleService service, ReceiptFactory factory)
		: base(Gtk.WindowType.Toplevel)
	{
		Build();
		this.service = service;
		this.factory = factory;
		service.BarcodeEvent += BarcodeHandler;
		service.CompleteSaleEvent += CompleteSaleHandler;
	}

	public void BarcodeHandler(object sender, ScannedBarcodeEventArgs args)
	{
		this.ItemsView.Buffer.Text += "\n" + args.ReadItem.ToString();
	}

	public void CompleteSaleHandler(object sender, CompleteSaleEventArgs e)
	{
		var receipt = factory.CreateReceiptFrom(e.Id, e.Items);
		this.ItemsView.Buffer.Text += "\n\n" + receipt.ToString();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnCompleteSaleClick(object sender, EventArgs e)
	{
		service.OnCompleteSale();
	}

	protected void OnScanButtonClick(object sender, EventArgs e)
	{
		service.OnBarcodeScan(this.ItemBarcodeField.Text);
	}

	protected void OnStartNewSale(object sender, EventArgs e)
	{
		this.ItemsView.Buffer.Clear();
	}
}
