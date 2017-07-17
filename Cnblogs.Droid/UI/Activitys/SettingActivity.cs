using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Cnblogs.Droid.UI.Shareds;
using Cnblogs.Droid.UI.Widgets;
using System;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Cnblogs.Droid.UI.Activitys
{
    [Activity(Label = "@string/setting", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class SettingActivity : BaseActivity, View.IOnClickListener
    {
        private Handler handler;
        private Toolbar toolbar;
        private UMengSharesWidget sharesWidget;

        protected override int LayoutResource => Resource.Layout.setting;
        public static void Start(Context context)
        {
            context.StartActivity(new Intent(context, typeof(SettingActivity)));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            handler = new Handler();

            StatusBarCompat.SetOrdinaryToolBar(this);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationIcon(Resource.Drawable.back_24dp);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            toolbar.SetNavigationOnClickListener(this);
            var packageInfo = this.PackageManager.GetPackageInfo(this.PackageName, 0);
            var txtVersion = FindViewById<TextView>(Resource.Id.txtVersion);
            txtVersion.Text = packageInfo.VersionName;
            FindViewById<LinearLayout>(Resource.Id.layoutSurvey).Click += (object sender, EventArgs e) =>
            {
                Intent intent = new Intent(Intent.ActionSendto);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.SetData(Android.Net.Uri.Parse("mailto:" + Resources.GetString(Resource.String.survey_email)));
                if (intent.ResolveActivity(this.PackageManager) != null)
                {
                    intent.PutExtra(Intent.ExtraSubject, "���� " + packageInfo.PackageName + " - " + packageInfo.VersionName + " �ķ������");
                    intent.PutExtra(Intent.ExtraText, "�豸��Ϣ��Android " + Build.VERSION.Release + " - " + Build.Manufacturer + " - " + Build.Model + "\n������漰��˽���ֶ�ɾ��������ݣ�\n\n");
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "ϵͳ��û�а�װ�ʼ��ͻ���", ToastLength.Short).Show();
                }
            };
            FindViewById<LinearLayout>(Resource.Id.layoutQQ).Click += (object sender, EventArgs e) =>
            {
                sharesWidget.Open("http://shang.qq.com/wpa/qunwpa?idkey=5c281d37638467fb0f411484dcd513b89ba82b58decb8518cc2523b95232dd9b", "����԰������APP��������Ⱥ");
            };
            FindViewById<LinearLayout>(Resource.Id.layoutOpenSourceUrl).Click += (object sender, EventArgs e) =>
            {
                sharesWidget.Open(Resources.GetString(Resource.String.open_source_url), "����԰������Android�ͻ��ˣ�Xamarin App��Material Design���");
            };
            sharesWidget = new UMengSharesWidget(this);
        }
        /// <summary>
        /// ��Ļ�������л�ʱ�������window leak������
        /// </summary>
        /// <param name="newConfig"></param>
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            sharesWidget.Close();
        }

        public void OnClick(View v)
        {
            ActivityCompat.FinishAfterTransition(this);
        }

    }
}