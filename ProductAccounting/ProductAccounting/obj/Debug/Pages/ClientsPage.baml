   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.PagesProductAccounting.Pages  5      )    #ProductAccounting.Pages.ClientsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:ProductAccounting.Pages     5        N� Title$  ClientsPage��5	       � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid��%<  '        '  �    �� 5   
   "  6   .�5      ��  � �� �#778FCB6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Century Gothic)�6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ Center=�6;   �� 5       � ��
 �   �6,   �� 5       � ��$ 1,1,1,1q�60   5      �� 5   
   "  6   .�5      ��   �@ HorizontalGridLinesBrush
 �   �69   �� 5       �@ VerticalGridLinesBrush
 �   �67   �� 5       �@ RowBackground �#E8F1FA6.   �� 5        �@ AlternatingRowBackground �#F1F1F169   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5       � �� �#A5A5A56,   �� 5       � ��$ 2q�60   5      5!      .��5"      �  � RowDefinitions 5#   
   �� 5$      $��40 �6   �� 5&      $��* �6   .+�5)   
   � $��0��6   .+�5*      �� -   5         ��Name$ Close��5*      $��Закрыть��6$   $	��100��66   ���#B0BEC5$��30��6W   $��	690,5,0,5q�6c   $
��Left=�6w   6�   �� 5+      -   5        $ Deletewarehouses��5+      $��Удалить��6/   ���#FF6F61$	��100��6V   $��30��6b   $��	585,5,0,5q�6n   $
��Left=�6�   6�   �� 5,      -   5        $ Addwarehouses��5,      $��Добавить��6,   ���#778FCB$	��100��6U   $��30��6a   $��	480,5,0,5q�6m   $
��Left=�6�   6�   �� 5-      -   5        $ Changewarehouses��5-      $��Изменить��6/   ���#778FCB$	��100��6X   $��30��6d   $��	375,5,0,5q�6p   $
��Left=�6�   6�   5.        50   
   -   5          Name$ clientsGrid��50         AutoGenerateColumns .  	   HeadersVisibility$	 Column=�6D   $��1��6_   
   Columns
 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$# Название склада��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *name+6a     53      $8 2Форма правовой организации��6%   $ * 6g    .  6I   �� *	organform+6q     54      $ 
Город��6%   $ * 6M    .  64   �� *city+6W     55      $ 
Адрес��6%   $ * 6P    .  64   �� *	address+6Z     56      $! Номер телефона��6%   $ * 6]    .  6=   �� *phonenumber+6g     57      $2 ,Адрес электронной почты��6%   $ * 6`    .  6F   �� *email+6j   59      5:      5;      