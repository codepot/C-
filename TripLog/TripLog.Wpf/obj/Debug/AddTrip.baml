   M S B A M L   `   `   `  ����I  ETripLog.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TripLog.Domain;assembly=TripLog.DomainTripLog.Domain  5   	   F BTripLog.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TripLog.WpfTripLog.Wpf 5   	      TripLog.Wpf.AddTrip   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Gdomain4clr-namespace:TripLog.Domain;assembly=TripLog.Domain     5   	   +localclr-namespace:TripLog.Wpf     5   	   -   5          � 
ResizeMode$  CanMinimize=�5	        � ShowInTaskbar .   � Title$ Add a New Trip��5
   	   $	��267��6    $	��500��6-    � WindowStartupLocation$ CenterOwner=�69   .��5      � $��0,6,0.333,2.667q�6   .+�5   
   � $
��Left=�6   $��26��6*   $��	17,20,0,0q�66   $	��Top=�6I   $	��100��6a    J� SnapsToDevicePixels . 6m   �� 5   
   -   5         ��Name$ label��5      $��	Trip Name��6   $
��Left=�63   $��	27,20,0,0q�6N   $	��Top=�6a   $��70��6y    . ��6�   Z� �� 5   
   -   5        $ 
label_Copy��5      $��Description��6$   $
��Left=�6:   $��	27,67,0,0q�6U   $	��Top=�6h   $��70��6�    . ��6�   Z� �� 5   
   -   5        $ label_Copy1��5      $��
Start Date��6%   $
��Left=�6:   $��
27,119,0,0q�6U   $	��Top=�6i   $��70��6�    . ��6�   Z� �� 5   
   -   5        $ label_Copy2��5      $��End Date��6%   $
��Left=�68   $��306,122,0,0q�6S   $	��Top=�6h   $��65��6�    . ��6�   Z� �� 5   
   -   5         ��Name$ textBox_tripName��5      $
��Left=�6,   $��36��6G   $��
107,24,0,0q�6S    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��365��6�    . $��16(�6�   ��6�   Z� .��6�   .��5      ��  �� Path$
 Name�5       5      5      �� 5   
   -   5        $ textBox_tripDescription��5      $
��Left=�63   $��36��6N   $��
107,71,0,0q�6Z   $
 Wrap=�6n   $	��Top=�6�   $	��365��6�    . $��13.333(�6�   ��6�   �� $ Description���6�   Z� 6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        	  Name$	 datePicker_startDate��5      $
��Left=�63   $��107,122,0,0q�6N   $	��Top=�6c    . $��26��6�   ��6�   Z� 
   SelectedDate
 6�   �� $ 	StartDate� �� Mode$ TwoWay=�  5   
   -	   5        $	 datePicker_EndDate��5      $
��Left=�61   $��371,122,0,0q�6L   $	��Top=�6a    . $��26��6�   ��6�   Z� 
 6�   �� $ EndDate�$ TwoWay=��� 5   
   -
   5         ��Name$ button_addTrip��5      $
��_Add��6)   $
��Left=�68   $��397,180,0,0q�6S   $	��Top=�6h   $��75��6�    .  �� 	IsDefault . $��32��6�   ��6�   Z� 6�   �� 5   
   -   5        $ button_cancel��5      $��_Cancel��6(   $
��Left=�6:   $��296,180,0,0q�6U   $	��Top=�6j   $��75��6�    .  �� IsCancel . $��32��6�   ��6�   Z� 6�   5      5      