   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   %    HotelReservations.Windows.Rooms   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Rooms��5   	   $	��450��6   $	��800��6$   .��5	      �  � RowDefinitions 5
   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   c� $��0��6   .@�5      �� -   5        $��AddBtn��5      $	��Add��6#   
�������6I   �� 5      -   5        $��EditBtn��5      $
��Edit��6$   
�������$��5,0,0,0q�6K   6]   �� 5      -   5        $��	DeleteBtn��5      $��Delete��6&   
�������$��5,0,0,0q�6O   6`   5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Room number:��6   $��Center=�6+   �� 5      -   5        $��RoomNumberSearchTB��5      $��24��60   $	��150��6<   $��3q�6H   $��Center=�6S   6u   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��RoomsDG��5      $��2��6"      
IsReadOnly$
 True��5      6%   5      5       