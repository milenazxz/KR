   M S B A M L   `   `   `  ����L  HProductAccounting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProductAccounting.FormsProductAccounting.Forms  5   	   /    )ProductAccounting.Forms.AuthorizationForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ProductAccounting.Forms     5   	     � Title$  AuthorizationForm��5   	   $	��400��6#   $	��700��60    � 	Resources 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   ;   �� 6
   6sys&clr-namespace:System;assembly=mscorlib     6;      KeyAvailableRoles   �� Type" ��6)   .s�5      ��Директор��6#   ��5      !Работник склада��6*   ��5      '#Экономист-аналитик��6-   5      .��5      � $��0,0,0,-6q�6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��196,107,366,165q�6,   .+�5      �� -   5         ��Name$ Login��5      $��
Логин��6#   $
��Bold"�63   $��20(�6E   �� 5      -   5        $ Password��5      $��Пароль��6&   $
��Bold"�67   $��20(�6I   �� 5      -   5        $
 Role��5      $��Роль��6"   $
��Bold"�61   $��20(�6C   5      �� 5   
   $��345,107,170,165q�6   .+�5      �� -   5         ��Name$ 
LoginField��5      $��0,5,0,23q�6*   G� 5      -   5         G�Name$ PasswordField��5      $��0,0,0,21q�62   �� 5      -   5         ��Name$ 	RoleField��5      $��0,0,0,10q�6*     AvailableRoles#��[ 6<   5      �� 5   
   -   5         ��Name$ AuthorizationBtn��5      $��
Войти��6+   $
��Bold"�6;   $��30��6M   $��277,271,241,89q�6x   5      5      