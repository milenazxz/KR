   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.PagesProductAccounting.Pages  5      +    %ProductAccounting.Pages.EmployeesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:ProductAccounting.Pages     5        N� Title$  EmployeesPage��5	       � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid��%<  '        '  �    �� 5   
   "  6   .�5      ��  � �� �#778FCB6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Century Gothic)�6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ Center=�6;   �� 5       � ��
 �   �6,   �� 5       � ��$ 1,1,1,1q�60   5      �� 5   
   "  6   .�5      ��   �@ HorizontalGridLinesBrush
 �   �69   �� 5       �@ VerticalGridLinesBrush
 �   �67   �� 5       �@ RowBackground �#E8F1FA6.   �� 5        �@ AlternatingRowBackground �#F1F1F169   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5       � �� �#A5A5A56,   �� 5       � ��$ 2q�60   5      5!      .��5"      �  � RowDefinitions 5#   
   �� 5$      $��40 �6   �� 5&      $��* �6    � ColumnDefinitions 5)   
   �� 5*   
   $��* �6   �� 5+   
   $
��Auto �6   .+�5-   
   � $��0��6   .+�5.      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��10,5,0,5q�6N   $��2��6`   .+�5/      �� -   5         ��Name$ AddEmployeeBtn��5/      $��Добавить��61   ���#778FCB$	��100��6Z   $��30��6f   $��0,0,10,0q�6r   $
��Left=�6�   6�   �� 50      -   5        $ ChangeEmployee��50      $��Изменить��61   ���#778FCB$	��100��6Z   $��30��6f   $��0,0,10,0q�6r   $
��Left=�6�   6�   �� 51      -   5        $ DeleteEmployeeBtn��51      $��Удалить��64   ���#FF6F61$	��100��6[   $��30��6g   $��0,0,10,0q�6s   $
��Left=�6�   6�   �� 52      -   5        $ Close��52      $��Закрыть��6(   $	��100��6:   ���#B0BEC5$��30��6[   $��0,0,10,0q�6g   $
��Left=�6z   6�   53      54        56   
   -   5        	  Name$	 employeersGrid��56      
   AutoGenerateColumns
 .     HeadersVisibility$ Column=�6G   $��1��6b      Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ ФИО��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6K      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *name+6U     59      $ Должность��6%   $ * 6Q    .  68   �� *post+6[     5:      $ Контакты��6%   $ * 6T    .  67   �� *
contacts+6^   5<      5=      5>      