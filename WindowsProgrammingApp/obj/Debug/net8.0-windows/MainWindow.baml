   M S B A M L   `   `   `  ����P  LWindowsProgrammingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WindowsProgrammingAppWindowsProgrammingApp  5	      ` \Syncfusion.PdfViewer.WPF, Version=25.2.7.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89mLclr-namespace:Syncfusion.Windows.PdfViewer;assembly=Syncfusion.PdfViewer.WPFSyncfusion.Windows.PdfViewer 5
      &     WindowsProgrammingApp.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :wpfx)http://schemas.xceed.com/wpf/xaml/toolkit     5      1muxc using:Microsoft.UI.Xaml.Controls     5      _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      /local#clr-namespace:WindowsProgrammingApp  5	      \	PdfViewerLclr-namespace:Syncfusion.Windows.PdfViewer;assembly=Syncfusion.PdfViewer.WPF  5
      ] YSyncfusion.Shared.WPF, Version=25.2.7.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c892
syncfusion!http://schemas.syncfusion.com/wpf  6d     � Title$  
MainWindow��5      $	��800��6   $
��1400��6%    � 	Resources 5         OpenMenuAnimation �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  CloseMenuAnimation��%|  &	        &	 �     �� 5   
   .��5      W� $ HiddenPanel��5      $8 2(UIElement.RenderTransform).(TranslateTransform.X)�5       2005       06   $ 0:0:0.3D�6#   5      �� 5   
   .��5      W� $ HiddenPanel��5      $8 2(UIElement.RenderTransform).(TranslateTransform.X)�5       05       2006   $ 0:0:0.3D�6#   5      5      .��5       � -   5         �Name$ myGrid��5        � ColumnDefinitions 5!   
   �� 5"      $��1* �6   $��10��6*   $	��600��68   �� 5#      $��5 �6   �� 5$      $��6* �6   $	��300��6*   .+�5*   
   �� $
��Auto=�6   .��5+      �� $��0��6   $��Stretch=�6)   .+�5-      �� 	 �� Orientation$	 
Horizontal=�6   $��44��66   
�������.+�5.      �� -   5        
 ��Name$
 NewFolderButton��5.      .��5/      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	FolderAdd=�63   50      �� 51      -   5        $
 NewFileButton��51      .��52        $ FilePlus=�63   53      54      R� 56      -   5         R�Name$ FoldersTreeView��56      $��Stretch=�64   $��Stretch=�6P   6�   59      5:      �� 5=   
   $��1��6   $��Stretch=�6(   $��Center=�6D   $��5��6a   
���   �6�   �� 5?   
   $
��Auto=�6   $��2��6:   $��Stretch=�6J   .��5@      �� $	 Vertical=�6   .+�5G      �� -   5         ��Name$ fileName��5G      
�������$��18(�6A    �� Padding$ 10q�6O   	test.xaml6g   �� 5H      -   5         ��Name$ settingText��5H   6   $	 
Horizontal=�6   .+�5I      �� -   5        .��5J        $ FormatFontSizeIncrease=�63   5K      �� 5L      -   5        .��5M        $ FormatFontSizeDecrease=�63   5N      �� 5O      -	   5         ��Name$ FontSizeComboBox��5O      $��50��5P       �� SelectedIndex$ 8��5Q      5R      �� 5T      -
   5        .��5V        $ FormatColorFill=�63   5W      +  %Syncfusion.Windows.Shared.ColorPicker  5X      -   5          Name$ myColorPicker��5X   -   �� 5Y      -   5        .��5Z        $ FormatColorHighlight=�63   5[      �� 5\      -   5        .��5]        $ FormatUnderline=�63   5^      �� 5_      -   5        .��5`        $ FormatStrikethrough=�63   5a      �� 5b      -   5        .��5c        $ 
FormatBold=�63   5d      �� 5e      -   5        .��5f        $ FormatItalic=�63   5g      �� 5h      -   5        .��5j        $ Keyboard=�63   5k      �� 5l      -   5        .��5m        $ ContentSave=�63   5n      5p      3  -Syncfusion.Windows.PdfViewer.PdfViewerControl  5q      -   5          Name$ 	pdfViewer��5q   -   $y�	Collapsed=�6@      ZoomMode$ FitWidth=�5r   	   5s      �� 5u      -   5         ��Name$ richTextBox��5x      $��Stretch=�5v      $��Stretch=�5w      " �� VerticalScrollBarVisibility$
 Auto=�5z      $ �� HorizontalScrollBarVisibility$
 Auto=�5{       �� IsDocumentEnabled$
 True��5}      . �5�      /� -   5         /�Name$
 flow��5�   #    �� 	AllowDrop$
 True��62   .T�5�      J� .*�6$   .*�5�      �� $#�텍스트를 입력하세요.��6"   5�      5�      5�      5�      5�      �� 5�   
   -   5        $��2��5�      $��Right=�6@   $��50��6\   $��0q�6h   .��5�        $ 	ArrowLeft=�6'   $��25��68   5�      �� 5�   
   -   5         ��Name$ splitter��5�   /   $y�	Collapsed=�6   $��3��6A   $��Stretch=�6Q   $��Center=�6m   $��5��6�   
���   �6�   � 5�   
   -   5        $ HiddenPanel��5�      $��4��6   
�������$y�	Collapsed=�6K   $��Right=�6b    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5�      T� 5�       T� X$ 0Q�6%    � RowDefinitions 5�      �� 5�      $��5* �6    �� 5�      $��1* �6    .+�5�      �� -   5        $��OutputRichTextBox��5�   '   $��0��6   $��16q�6@    �� 
IsReadOnly$
 True��6M   �� 5�      -   5          ��Name$  InputTextBlock��5�      $��16q�6.   $��1��6;   
�������$��질문��6_   ! �� TextWrapping$
! Wrap=�6j   $��16(�6~   �� 5�      -   5        $
 CopyTextButton��5�   �   $��Bottom=�6   $��Right=�61   $��10q�6N   $��1��6Z   .��6�   질문6�   5�      5�      5�      