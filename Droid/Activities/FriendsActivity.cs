﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamSnap.Droid
{
    [Activity(Label = "Friends")]
    public class FriendsActivity : BaseActivity<FriendViewModel>
    {
        ListView listView;
        Adapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Friends);
            listView = FindViewById<ListView>(Resource.Id.friendsList);
            listView.Adapter =
              adapter = new Adapter(this);
        }

        protected async override void OnResume()
        {
            base.OnResume();
            try
            {
                await viewModel.GetFriends();
                adapter.NotifyDataSetInvalidated();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }   

        class Adapter : BaseAdapter<User>
        {
            readonly FriendViewModel friendViewModel =
              ServiceContainer.Resolve<FriendViewModel>();
            readonly LayoutInflater inflater;

            public Adapter(Context context)
            {
                inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = inflater.Inflate(Resource.Layout.FriendListItem, null);
                }
                var friend = this[position];
                var friendname = convertView.FindViewById<TextView>(Resource.Id.friendName);
                friendname.Text = friend.Name;
                return convertView;
            }

            public override int Count
            {
                get
                {
                    return friendViewModel.Friends == null ? 0 : friendViewModel.Friends.Length;
                }
            }

            public override User this[int position]
            {
                get { return friendViewModel.Friends[position]; }
            }
        }
    }
}

