   M S B A M L   `   `   `  ����F  BTripLog.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TripLog.WpfTripLog.Wpf  5   	       TripLog.Wpf.EditTrip   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TripLog.Wpf     5   	   -   5          � 
ResizeMode$  CanMinimize=�5       � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5        � Title$ 	Edit Trip��5	   	   $	��261��6   $��488.1��6(   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��216* �6   .+�5   
   � $
��Left=�6   $��26��6*   $��	10,10,0,0q�66   $	��Top=�6I   $	��100��6a    J� SnapsToDevicePixels . $��2��6�   �� 5   
   -   5         ��Name$ label��5      $��	Trip Name��6   $
��Left=�63   $��	20,10,0,0q�6N   $	��Top=�6a   $��70��6y    . $��2��6�   ��6�   Z� 6�   �� 5   
   -   5        $ 
label_Copy��5      $��Description��6$   $
��Left=�6:   $��	20,57,0,0q�6U   $	��Top=�6h   $��70��6�    . $��2��6�   ��6�   Z� 6�   �� 5   
   -   5        $ label_Copy1��5      $��
Start Date��6%   $
��Left=�6:   $��
20,109,0,0q�6U   $	��Top=�6i   $��70��6�    . $��2��6�   ��6�   Z� 6�   �� 5   
   -   5        $ label_Copy2��5      $��End Date��6%   $
��Left=�68   $��249,112,0,0q�6S   $	��Top=�6h   $��65��6�    . $��1��6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ textBox_tripName��5      $
��Left=�6,   $��36��6G   $��	50,14,0,0q�6S    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��365��6�    . $��16(�6�   $��1��6�   ��6�   Z� ��6�   �� *Name+6�   �� 5   
   -   5        $ textBox_tripDescription��5      $
��Left=�63   $��36��6N   $��	50,61,0,0q�6Z   $
 Wrap=�6m   $	��Top=�6�   $	��365��6�    . $��13.333(�6�   $��1��6  ��6�   �� *Description+��6�   Z� 6  (  "System.Windows.Controls.DatePicker  5   
   -   5        	  Name$	 datePicker_startDate��5      $
��Left=�63   $��
50,112,0,0q�6N   $	��Top=�6b    . $��26��6�   $��1��6�   ��6�   Z� 
   SelectedDate
 6�   �� *	StartDate+ �� Mode$ TwoWay=�6�     5   
   -	   5        $	 datePicker_EndDate��5      $
��Left=�61   $��314,112,0,0q�6L   $	��Top=�6a    . $��26��6�   $��1��6�   ��6�   Z� 
 6�   �� *	EndDate+$ TwoWay=�6�   �� 5   
   -
   5         ��Name$ button_updateTrip��5      $��_Update��6,   $
��Left=�6>   $��340,170,0,0q�6Y   $	��Top=�6n   $��75��6�    .  �� 	IsDefault . $��32��6�   $��1��6�    � ToolTip$ Update  Current Trip��6  ��6�   Z� 6  �� 5   
   -   5        $ button_cancel��5      $��_Cancel��6(   $
��Left=�6:   $��130,170,0,0q�6U   $	��Top=�6j   $��75��6�    .  �� IsCancel . $��32��6�   $��1��6�   $ Cancel and close��6  ��6�   Z� 6  �� 5   
   -   5        $ button_deleteTrip��5      $��_Delete��6,   $
��Left=�6>   $��237,170,0,0q�6Y   $	��Top=�6n   $��75��6�    .  . $��32��6�   $��1��6�   $ Delete Current Trip��6  ��6�   Z� 6  5      5      