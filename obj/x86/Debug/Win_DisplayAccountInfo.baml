   M S B A M L   `   `   `  ����>  :APP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	   ,    &WpfApplication1.Win_DisplayAccountInfo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	   -   5          � Title$  Win_DisplayAccountInfo��5   	   $	��600��5   	   $	��800��6    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�62    � Topmost .  � WindowState$ 	Maximized=�5       � Icon$ _MDS9000.ico��60   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $	��.5* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $	��.5* �6   �� 5       $
��1.5* �6   �� 5!      $	��.5* �6   �� 5"      $
��1.5* �6   �� 5#      $	��.5* �6   �� 5$      $
��1.5* �6   �� 5%      $	��.5* �6   �� 5&      $
��1.5* �6   �� 5'      $	��.5* �6   �� 5(      $
��1.5* �6   �� 5)      $��1* �6   .+�5,   
   �� $��1��6   $��9��6   $��1��61   $��Verify the details below��6A   $��50(�6d   $��Center=�6r   �� 5.   
   $��5��6   $��9��6   $��1��61   $��Account Number:��6A   $��35(�6[   $��Center=�6i   �� 5/   
   -   5        $��txtAccNo��5/      $��2��6"   $��7��62   $��7��6F   $��1��6S   $��35(�6e   	 �� 
IsReadOnly	 . 
 �� TextAlignment$
 Center=�6�   $�� ��6�   $��3q�6�   �� 51   
   -   5        $��
btnConfirm��51      $��1��6#   $��4��63   $��11��6G   $��1��6U   $��25(�6f   
��� � �
�������.��6�   	CONFIRM6�   �� 52   
   -   5        $��	btnCancel��52      $��6��6"   $��4��62   $��11��6F   $��1��6T   $��25(�6e   
���  ��
�������.��6�   CANCEL6�   53      54      