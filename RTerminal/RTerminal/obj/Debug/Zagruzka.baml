   M S B A M L   `   `   `  ����D  @RTerminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:RTerminal	RTerminal  5   	       RTerminal.Zagruzka   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:RTerminal     5   	   -   5          � Title$  Sleep_Window��5   	   $	��180��6   $	��350��6+   
������� J� Opacity$	 0.9Q�5	       � WindowStyle$
 None=�5
   	    � WindowStartupLocation$ CenterScreen=�6    � AllowsTransparency .  � Topmost . .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��60 �6   .+�5   
   � $��0��6   .+�5      �� -   5         ��Name$ canvas��5      $	��115��6%   $	��115��61   $��Center=�6>   $��Center=�6[    J� RenderTransformOrigin$ 0.5,0.5&�6v   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5      5      	 � Triggers	 5      :� 5      
 :� RoutedEvent$
 Loaded��6#   .[�5      �� .��5      �� .��5   "   U�  �� TargetProperty$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�6@    �� 
TargetName$ canvas��6�    g� RepeatBehavior$ Forever��6�   .\�5   &   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0��6;    O� Value$ 0Q�6G     5    &   $ 0:0:2��6;   $	 360Q�6K   5!   #   5"      5#      5$      .+�5&      A� ���#0A6E9E$��20��6)    �� Left$
 46.5��65   $��20��6H   
 �� Top$ 83��6S   A� 5'      ���#0A6E9E$��20��6)   $
 80.5��65   $��20��6H   $ 49��6S   $	 0.6Q�6c   A� 5(      ���#0A6E9E$��20��6)   $
 46.5��65   $��20��6H   $ 17��6S   $	 0.4Q�6c   A� 5)      ���#0A6E9E$��20��6)   $
 14.5��65   $��20��6H   $ 49��6S   $	 0.2Q�6c   A� 5*      ���#0A6E9E$��20��6)   $
 23.5��65   $��20��6H   $ 72��6S   $	 0.1Q�6c   A� 5+      ���#0A6E9E$��20��6)   $
 70.5��65   $��20��6H   $ 73��6S   $	 0.8Q�6c   A� 5,      ���#0A6E9E$��20��6)   $
 69.5��65   $��20��6H   $ 26��6S   $	 0.5Q�6c   A� 5-      ���#0A6E9E$��20��6)   $
 23.5��65   $��20��6H   $ 27��6S   $	 0.3Q�6c   5/      50      � 51   
   $��1��6   .+�52      �� $��Подождите !!!��6   $��30(�6-   $	��Top=�6;   $��Center=�6S   ���#0A6E9E$��Medium"�6�   53      54      55      