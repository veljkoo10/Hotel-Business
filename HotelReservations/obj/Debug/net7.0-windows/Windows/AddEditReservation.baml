   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5      2    ,HotelReservations.Windows.AddEditReservation   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9local'clr-namespace:HotelReservations.Windows     5      -   5          �Name$  AddEditReservationWindow��5       � Title$ AddEditReservation��5	      $	��450��6!   $	��800��6.   .��5
      � .+�5   
   � $	��400��6   $��Center=�6   $��Center=�68   $	��314��6S   .+�5      �� $��10,10,10,20q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�5      �� $��RoomNumber :��6    $��Center=�64   $	��120��6O   �� 5      -   5        $��RoomNumberComboBox��5      $
��Left=�69   $	��200��6T    �� SelectedItem 6`   ��  �� Path$ 
RoomNumber�6�   5      �� 5      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�5      �� $��Guest :��6    $��Center=�6/   $	��120��6J   �� 5      -   5        $��GuestListBox��5      $
��Left=�62   $	��200��6M    �� SelectionMode$ Multiple=�6�    6Y   �� *Guest+ �� Mode$ TwoWay=���5      �� 5      .R�5   "   �� ��6,   �� 5      5      5       �� 5"      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�5#      �� $��ReservationType :��6    $��Center=�69   $	��120��6T   �� 5$      -   5        $��ReservationTypeCB��5$      $
��Left=�68   $	��200��6S   ${�False��6�    6_   �� *ReservationType+6�   5%      �� 5'      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�5(      �� $��Start Date Time :��6    $��Center=�69   $	��120��6T   (  "System.Windows.Controls.DatePicker  5)      -   5        $��StartDateTimePicker��5)   !   $
��Left=�6<   $	��200��6W      SelectedDate 6c   �� *StartDateTime+6�   5*      �� 5,      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�5-      �� $��End Date Time :��6    $��Center=�67   $	��120��6R     5.      -   5        $��EndDateTimePicker��5.   !   $
��Left=�6:   $	��200��6U    6a   �� *EndDateTime+6�   5/      �� 51      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�52      �� $��Total value :��6    $��Center=�65   $	��120��6P   �� 53      -   5        $��TotalValueTextBox��53      $
��Left=�67   $	��200��6R   ${�False��6   ��6^   �� $ 
TotalPrice�6   54      59      � 5:      $��20��6   5;      �� 5=   
   $ 
Horizontal=�6   $��Bottom=�6.   $��Right=�6I   $��	0,0,20,20q�6e   .+�5>      �� -   5        $��Cancel��5>      $��80��6&   $��0,0,10,0q�61   6C   �� 5?      -	   5        $
��Save��5?      $��80��6$   6/   5@      5A      5B      