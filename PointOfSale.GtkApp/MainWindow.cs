using System;
using System.Collections.Generic;
using GLib;
using Gtk;
using PointOfSale.Domain;

public partial class MainWindow : Gtk.Window, Display
{
	private readonly PointOfSaleService service;
	private readonly ReceiptFactory factory;
	private readonly ListStore items;

	public MainWindow(PointOfSaleService service, ReceiptFactory factory)
		: base(Gtk.WindowType.Toplevel)
	{
		Build();
		this.service = service;
		this.factory = factory;
		this.items = new ListStore(typeof(string), typeof(string), typeof(string));
		this.service.BarcodeEvent += BarcodeHandler;
		this.service.CompleteSaleEvent += CompleteSaleHandler;

		SetupWidgets();
	}

	private void SetupWidgets()
	{
		SetupTableData();
		SetupBarcodeFieldHandlers();
	}

	private void SetupTableData()
	{
		var itemBarcodeCol = new TreeViewColumn();
		var itemNameCol = new TreeViewColumn();
		var itemPriceCol = new TreeViewColumn();

		itemBarcodeCol.Title = "Barcode";
		itemNameCol.Title = "Name";
		itemPriceCol.Title = "Price";

		var itemNameText = new CellRendererText();
		var itemBarcodeText = new CellRendererText();
		var itemPriceText = new CellRendererText();

		itemBarcodeCol.PackEnd(itemBarcodeText, true);
		itemNameCol.PackEnd(itemNameText, true);
		itemPriceCol.PackEnd(itemPriceText, true);

		itemBarcodeCol.AddAttribute(itemBarcodeText, "text", 0);
		itemNameCol.AddAttribute(itemNameText, "text", 1);
		itemPriceCol.AddAttribute(itemPriceText, "text", 2);

		this.ScannedItemTable.AppendColumn(itemBarcodeCol);
		this.ScannedItemTable.AppendColumn(itemNameCol);
		this.ScannedItemTable.AppendColumn(itemPriceCol);

		ScannedItemTable.Model = items;
	}

	private void SetupBarcodeFieldHandlers()
	{
		this.ItemBarcodeField.Activated += delegate
		{
			try
			{
				service.OnBarcodeScan(ItemBarcodeField.Text);
			}
			catch (IndexOutOfRangeException e)
			{
				PopupErrorMessage(e);
			};
			ItemBarcodeField.Text = "";
		};
	}

	private void PopupErrorMessage(IndexOutOfRangeException e)
	{
		using (var errorDialog = new MessageDialog(this,
										  DialogFlags.DestroyWithParent,
										  MessageType.Error,
										  ButtonsType.Close,
										  e.Message))
		{
			errorDialog.Run();
			errorDialog.ShowAll();
			errorDialog.Hide();
		}
	}

	public void BarcodeHandler(object sender, ScannedBarcodeEventArgs args)
	{
		items.AppendValues(args.ReadItem.Barcode, args.ReadItem.Name, args.ReadItem.Price.ToString());
	}

	public void CompleteSaleHandler(object sender, CompleteSaleEventArgs e)
	{
		var receipt = factory.CreateReceiptFrom(e.Id, e.Items);
		PopupDialog(receipt);
		this.items.Clear();
	}

	private void PopupDialog(Receipt receipt)
	{
		using (var md = new MessageDialog(this,
										  DialogFlags.DestroyWithParent,
										  MessageType.Info,
										  ButtonsType.Close,
										  receipt.ToString()))
		{
			md.Run();
			md.ShowAll();
			md.Hide();
		}
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
}
