   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.FormsProductAccounting.Forms  5      +    %ProductAccounting.Forms.WriteOffsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:ProductAccounting.Forms     5        N� Title$  WriteOffsPage��5	       � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid��%<  '        '  �    �� 5   
   "  6   .�5      ��  � �� �#778FCB6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Century Gothic)�6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ Center=�6;   �� 5       � ��
 �   �6,   �� 5       � ��$ 1,1,1,1q�60   5      �� 5   
   "  6   .�5      ��   �@ HorizontalGridLinesBrush
 �   �69   �� 5       �@ VerticalGridLinesBrush
 �   �67   �� 5       �@ RowBackground �#E8F1FA6.   �� 5        �@ AlternatingRowBackground �#F1F1F169   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5       � �� �#A5A5A56,   �� 5       � ��$ 2q�60   5      5!      .��5#      �  � RowDefinitions 5$   
   �� 5%      $��40 �6   �� 5'      $��* �6   .+�5*   
   � $��0��6    � ColumnDefinitions 5+      �� 5,      $��* �6#   �� 5-      $
��Auto �6#   .+�5/      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��
10,5,-10,5q�6N   $��2��6b   .+�50      �� -   5         ��Name$ AddWriteOffsBtn��50      $��Добавить��62   ���#778FCB$	��100��6[   $��30��6g   $��0,0,10,0q�6t   $
��Left=�6�   6�   �� 51      -   5        $ DeleteWriteOffsBtn��51      $��Удалить��65   ���#FF6F61$	��100��6\   $��30��6h   $��0,0,10,0q�6u   $
��Left=�6�   6�   �� 52      -   5        $ Close��52      $��Закрыть��6(   $	��100��6:   ���#B0BEC5$��30��6[   $��0,0,10,0q�6h   $
��Left=�6{   6�   53      54        56   
   -   5        	  Name$	 writeOffsGrid��56      
   AutoGenerateColumns
 .     HeadersVisibility$ Column=�6F   $��1��6a      Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ Сотрудник��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6a      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *IdEmpNavigation.name+6k     59      $ 
Склад��6%   $ * 6_    .  64   �� *IdWarehNavigation.name+6i     5:      $ Дата��6%   $ * 6L    .  63   �� *date+6V   5<      5=      5>      