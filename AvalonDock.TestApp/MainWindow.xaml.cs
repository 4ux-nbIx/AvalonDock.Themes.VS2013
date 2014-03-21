﻿/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock.Layout;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using System.Resources;
using System.Collections;
using Xceed.Wpf.AvalonDock;

namespace AvalonDock.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


            DispatcherTimer timer = new DispatcherTimer();
            Random rnd = new Random();
            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Tick += (s, e) =>
                {
                    TestTimer++;

                    TestBackground = new SolidColorBrush(Color.FromRgb(
                        (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));

                    FocusedElement = Keyboard.FocusedElement == null ? string.Empty : Keyboard.FocusedElement.ToString();
                    //Debug.WriteLine(string.Format("ActiveContent = {0}", dockManager.ActiveContent));

                };
            timer.Start();

            this.DataContext = this;

            winFormsHost.Child = new UserControl1();

        }

        #region TestTimer

        /// <summary>
        /// TestTimer Dependency Property
        /// </summary>
        public static readonly DependencyProperty TestTimerProperty =
            DependencyProperty.Register("TestTimer", typeof(int), typeof(MainWindow),
                new FrameworkPropertyMetadata((int)0));

        /// <summary>
        /// Gets or sets the TestTimer property.  This dependency property 
        /// indicates a test timer that elapses evry one second (just for binding test).
        /// </summary>
        public int TestTimer
        {
            get { return (int)GetValue(TestTimerProperty); }
            set { SetValue(TestTimerProperty, value); }
        }

        #endregion

        #region TestBackground

        /// <summary>
        /// TestBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty TestBackgroundProperty =
            DependencyProperty.Register("TestBackground", typeof(Brush), typeof(MainWindow),
                new FrameworkPropertyMetadata((Brush)null));

        /// <summary>
        /// Gets or sets the TestBackground property.  This dependency property 
        /// indicates a randomly changing brush (just for testing).
        /// </summary>
        public Brush TestBackground
        {
            get { return (Brush)GetValue(TestBackgroundProperty); }
            set { SetValue(TestBackgroundProperty, value); }
        }

        #endregion

        #region FocusedElement

        /// <summary>
        /// FocusedElement Dependency Property
        /// </summary>
        public static readonly DependencyProperty FocusedElementProperty =
            DependencyProperty.Register("FocusedElement", typeof(string), typeof(MainWindow),
                new FrameworkPropertyMetadata((IInputElement)null));

        /// <summary>
        /// Gets or sets the FocusedElement property.  This dependency property 
        /// indicates ....
        /// </summary>
        public string FocusedElement
        {
            get { return (string)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        #endregion

        private void OnLayoutRootPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var activeContent = ((LayoutRoot)sender).ActiveContent;
            if (e.PropertyName == "ActiveContent")
            {
                Debug.WriteLine(string.Format("ActiveContent-> {0}", activeContent));
            }
        }

        private void OnLoadLayout(object sender, RoutedEventArgs e)
        {
            var currentContentsList = dockManager.Layout.Descendents().OfType<LayoutContent>().Where(c => c.ContentId != null).ToArray();

            string fileName = (sender as MenuItem).Header.ToString();
            var serializer = new XmlLayoutSerializer(dockManager);
            //serializer.LayoutSerializationCallback += (s, args) =>
            //    {
            //        var prevContent = currentContentsList.FirstOrDefault(c => c.ContentId == args.Model.ContentId);
            //        if (prevContent != null)
            //            args.Content = prevContent.Content;
            //    };
            using (var stream = new StreamReader(string.Format(@".\AvalonDock_{0}.config", fileName)))
                serializer.Deserialize(stream);
        }

        private void OnSaveLayout(object sender, RoutedEventArgs e)
        {
            string fileName = (sender as MenuItem).Header.ToString();
            var serializer = new XmlLayoutSerializer(dockManager);
            using (var stream = new StreamWriter(string.Format(@".\AvalonDock_{0}.config", fileName)))
                serializer.Serialize(stream);
        }

        private void OnShowWinformsWindow(object sender, RoutedEventArgs e)
        {
            var winFormsWindow = dockManager.Layout.Descendents().OfType<LayoutAnchorable>().Single(a => a.ContentId == "WinFormsWindow");
            if (winFormsWindow.IsHidden)
                winFormsWindow.Show();
            else if (winFormsWindow.IsVisible)
                winFormsWindow.IsActive = true;
            else
                winFormsWindow.AddToLayout(dockManager, AnchorableShowStrategy.Bottom | AnchorableShowStrategy.Most);
        }

        private void AddTwoDocuments_click(object sender, RoutedEventArgs e)
        {
            var firstDocumentPane = dockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {
                LayoutDocument doc = new LayoutDocument();
                doc.Title = "Test1";
                firstDocumentPane.Children.Add(doc);

                LayoutDocument doc2 = new LayoutDocument();
                doc2.Title = "Test2";
                firstDocumentPane.Children.Add(doc2);
            }

            var leftAnchorGroup = dockManager.Layout.LeftSide.Children.FirstOrDefault();
            if (leftAnchorGroup == null)
            {
                leftAnchorGroup = new LayoutAnchorGroup();
                dockManager.Layout.LeftSide.Children.Add(leftAnchorGroup);
            }

            leftAnchorGroup.Children.Add(new LayoutAnchorable() { Title = "New Anchorable" });

        }

        private void OnShowToolWindow1(object sender, RoutedEventArgs e)
        {
            var toolWindow1 = dockManager.Layout.Descendents().OfType<LayoutAnchorable>().Single(a => a.ContentId == "toolWindow1");
            if (toolWindow1.IsHidden)
                toolWindow1.Show();
            else if (toolWindow1.IsVisible)
                toolWindow1.IsActive = true;
            else
                toolWindow1.AddToLayout(dockManager, AnchorableShowStrategy.Bottom | AnchorableShowStrategy.Most);
        }

        private void dockManager_DocumentClosing(object sender, DocumentClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the document?", "AvalonDock Sample", MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
          // Uncomment when TRACE is activated on AvalonDock project
            // dockManager.Layout.ConsoleDump(0);
        }

        private void OnReloadManager(object sender, RoutedEventArgs e)
        {
        }

        private void OnUnloadManager(object sender, RoutedEventArgs e)
        {
            if (layoutRoot.Children.Contains(dockManager))
                layoutRoot.Children.Remove(dockManager);
        }

        private void OnLoadManager(object sender, RoutedEventArgs e)
        {
            if (!layoutRoot.Children.Contains(dockManager))
                layoutRoot.Children.Add(dockManager);
        }

        private void OnToolWindow1Hiding(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to hide this tool?", "AvalonDock", MessageBoxButton.YesNo)== MessageBoxResult.No)
                e.Cancel = true;
        }


    }
}
