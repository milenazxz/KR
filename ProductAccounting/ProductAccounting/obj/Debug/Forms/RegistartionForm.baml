   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.FormsProductAccounting.Forms  5   	   .    (ProductAccounting.Forms.RegistartionForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ProductAccounting.Forms     5   	     � Title$  RegistartionForm��5   	   $	��530��6"   $	��900��6/    � WindowStartupLocation$ CenterScreen=�6;    � 
ResizeMode$ NoResize=�6`    � 	Resources 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   ;   �� 6
   6sys&clr-namespace:System;assembly=mscorlib     6;      KeyAvailableRoles   �� Type" ��6)   .s�5      ��Директор��6#   ��5      !Работник склада��6*   ��5      '#Экономист-аналитик��6-   5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��159,104,443,164q�6,   .+�5      �� -   5         ��Name$
 Name��5      $��ФИО��6"   $
��Bold"�60   $��20(�6B   �� 5      -   5        $ Login��5      $��
Логин��6#   $
��Bold"�63   $��20(�6E   �� 5      -   5        $
 Role��5      $��Роль��6"   $
��Bold"�61   $��20(�6C   �� 5      -   5        $ Contacts��5      $��Контакты��6&   $
��Bold"�69   $��20(�6K   �� 5      -   5        $ 	Password1��5      $��Пароль��6'   $
��Bold"�68   $��20(�6J   �� 5      -   5        $ 	Password2��5      $%��Повторите пароль��6'   $
��Bold"�6B   $��20(�6T   5      �� 5   
   $��400,110,163,164q�6   .+�5      �� -   5         ��Name$ 	NameField��5      $��0,0,0,7q�6d   $��23��6u   ��6)   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�6u   �� 5      -   5        $ 
LoginField��5      $��0,5,0,11q�6f   $��23��6x   ��6*   �� *Login+$ PropertyChanged=�6x   �� 5      -	   5        	 ��Name$	 	RoleField��5        AvailableRoles#��[ 6e   $��23��6�   $��0,2,0,10q�6�   
 �� Text
 6*   �� *Post+$ PropertyChanged=�6�   �� 5      -
   5        $ ContactsField��5      $��0,5,0,15q�6l   $��23��6~   ��6-   �� *
Contacts+$ PropertyChanged=�6~   G� 5      -   5         G�Name$ PasswordField1��5      $��0,0,0,15q�62   $��23��6D   G� 5       -   5        $ PasswordField2��5       $��23��62   5!      �� 5#      -   5         ��Name$ RegistrationBtn��5#      $��Подтвердить��6.   $
��Bold"�6D   $��30��6V   $	��200��6b   $��300,375,300,79q�6�   5%      5&      