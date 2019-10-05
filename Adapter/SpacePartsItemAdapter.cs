using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CheckStockApp
{
    class SpacePartsItemAdapter : BaseAdapter
    {

        Context context;
        List<SpacePartsList.SpacePart> spacePartList;
        private int positions;
        private int roundcount;

        public SpacePartsItemAdapter(Context context, List<SpacePartsList.SpacePart> spacePartList ,int roundcounts)
        {
            this.context = context;
            this.spacePartList = spacePartList;
            this.roundcount = roundcounts;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Typeface openSansRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Prompt-Light.ttf");
            var view = convertView;
            positions = position;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.cell_spacepart_item, parent, false);
                var statuscount = view.FindViewById<TextView>(Resource.Id.statusCountTextView);
                var self_main = view.FindViewById<TextView>(Resource.Id.selfMainTextView);
                var id_item = view.FindViewById<TextView>(Resource.Id.idItemTextView);
                var name_item = view.FindViewById<TextView>(Resource.Id.name_textView);
                var group_item = view.FindViewById<TextView>(Resource.Id.groupItem_textView);
                var totalstock_item = view.FindViewById<TextView>(Resource.Id.totalstock_textView);
                var costunit_item = view.FindViewById<TextView>(Resource.Id.costunit_textView);
                var f_group_item = view.FindViewById<TextView>(Resource.Id.f_groupItem_textView);
                var f_totalstock_item = view.FindViewById<TextView>(Resource.Id.f_totalstock_textView);
                var f_costunit_item = view.FindViewById<TextView>(Resource.Id.f_costunit_textView);

                statuscount.Typeface = openSansRegular;
                self_main.Typeface = openSansRegular;
                id_item.Typeface = openSansRegular;
                name_item.Typeface = openSansRegular;
                group_item.Typeface = openSansRegular;
                totalstock_item.Typeface = openSansRegular;
                costunit_item.Typeface = openSansRegular;
                f_group_item.Typeface = openSansRegular;
                f_totalstock_item.Typeface = openSansRegular;
                f_costunit_item.Typeface = openSansRegular;

                view.Tag = new ViewHolder() { StatusCount_TextView= statuscount, SelfMain_TextView = self_main ,IDItem_TextView = id_item, NameItem_TextView = name_item, GroupItem_TextView = group_item, TotalStock_TextView = totalstock_item, CostPriceUnit_TextView = costunit_item};

            }

            var holder = (ViewHolder)view.Tag;

            //fill in your items
            this.checkStatusCount(holder);
            holder.SelfMain_TextView.Text = "ชั้นวาง : " + spacePartList[position].Self_Main.ToString();
            holder.IDItem_TextView.Text = "รหัสสินค้า : " + spacePartList[position].ID_Item.ToString();
            holder.NameItem_TextView.Text = "ชื่อสินค้า : " + spacePartList[position].Name_Item.ToString();
            holder.GroupItem_TextView.Text = spacePartList[position].Group_Item.ToString();
            holder.CostPriceUnit_TextView.Text = spacePartList[position].Cost_Price_Unit.ToString().ToString();
            holder.TotalStock_TextView.Text = spacePartList[position].Total_Stock.ToString().ToString();

            return view;
        }
        public void checkStatusCount(ViewHolder holder)
        {
            switch (roundcount)
            {
                case 1:
                    if (Convert.ToInt16(spacePartList[positions].Count1) == 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : ยังไม่ได้นับ";
                        holder.StatusCount_TextView.SetTextColor(Color.OrangeRed);
                        holder.IDItem_TextView.SetTextColor(Color.OrangeRed);
                    }
                    else if (Convert.ToInt16(spacePartList[positions].Count1) != 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : นับแล้ว";
                        holder.StatusCount_TextView.SetTextColor(Color.LightGreen);
                        holder.IDItem_TextView.SetTextColor(Color.LightGreen);
                    }

                    break;
                case 2:
                    if (Convert.ToInt16(spacePartList[positions].Count2) == 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : ยังไม่ได้นับ";
                        holder.StatusCount_TextView.SetTextColor(Color.OrangeRed);
                        holder.IDItem_TextView.SetTextColor(Color.OrangeRed);
                    }
                    else if (Convert.ToInt16(spacePartList[positions].Count2) != 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : นับแล้ว";
                        holder.StatusCount_TextView.SetTextColor(Color.LightGreen);
                        holder.IDItem_TextView.SetTextColor(Color.LightGreen);
                    }

                    break;
                case 3:
                    if (Convert.ToInt16(spacePartList[positions].Count3) == 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : ยังไม่ได้นับ";
                        holder.StatusCount_TextView.SetTextColor(Color.OrangeRed);
                        holder.IDItem_TextView.SetTextColor(Color.OrangeRed);
                    }
                    else if (Convert.ToInt16(spacePartList[positions].Count3) != 0)
                    {
                        holder.StatusCount_TextView.Text = "สถานะ : นับแล้ว";
                        holder.StatusCount_TextView.SetTextColor(Color.LightGreen);
                        holder.IDItem_TextView.SetTextColor(Color.LightGreen);
                    }

                    break;
            }
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return spacePartList.Count;
            }
        }
    }

    class ViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView StatusCount_TextView { get; set; }
        public TextView SelfMain_TextView { get; set; }
        public TextView IDItem_TextView { get; set; }
        public TextView NameItem_TextView { get; set; }
        public TextView GroupItem_TextView { get; set; }
        public TextView CostPriceUnit_TextView { get; set; }
        public TextView TotalStock_TextView { get; set; }
    }
}