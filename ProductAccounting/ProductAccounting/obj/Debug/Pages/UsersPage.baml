   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.PagesProductAccounting.Pages  5      '    !ProductAccounting.Pages.UsersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:ProductAccounting.Pages     5        N� Title$  	UsersPage��5	       � 	Resources 5
      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid��%<  '        '  �    �� 5   
   "  6   .�5      ��  � �� �#778FCB6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Century Gothic)�6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ Center=�6;   �� 5       � ��
 �   �6,   �� 5       � ��$ 1,1,1,1q�60   5      �� 5   
   "  6   .�5      ��   �@ HorizontalGridLinesBrush
 �   �69   �� 5       �@ VerticalGridLinesBrush
 �   �67   �� 5       �@ RowBackground �#E8F1FA6.   �� 5        �@ AlternatingRowBackground �#F1F1F169   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5       � �� �#A5A5A56,   �� 5       � ��$ 2q�60   5      5       .��5!      �  � RowDefinitions 5"   
   �� 5#      $��40 �6   �� 5%      $��* �6   .+�5(   
   � $��0��6   .+�5)      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��
10,5,-10,5q�6N   $��2��6b   .+�5*      �� -   5         ��Name$ 
AddItemBtn��5*      $��Добавить��6-   ���#778FCB$	��100��6V   $��30��6b   $��0,0,10,0q�6n   $
��Left=�6�   6�   �� 5+      -   5        $ ChangeItemBtn��5+      $��Изменить��60   ���#778FCB$	��100��6Y   $��30��6e   $��0,0,10,0q�6q   $
��Left=�6�   6�   �� 5,      -   5        $ DeleteItemBtn��5,      $��Удалить��60   ���#FF6F61$	��100��6W   $��30��6c   $��0,0,10,0q�6o   $
��Left=�6�   6�   �� 5-      -   5        $ Close��5-      $��Закрыть��6(   $	��100��6:   ���#B0BEC5$��30��6[   $��0,0,10,0q�6g   $
��Left=�6z   6�   5.      5/        51   
   -   5          Name$ 	UsersGrid��51      	   AutoGenerateColumns	 .  
   HeadersVisibility$
 Column=�6B   $��1��6]      Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ ФИО��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6K      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *name+6U     54      $ Должность��6%   $ * 6Q    .  68   �� *post+6[     55      $ Контакты��6%   $ * 6T    .  67   �� *
contacts+6^     56      $ 
Логин��6%   $ * 6N    .  64   �� *login+6X   58      59      5:      