   M S B A M L   `   `   `  ����H  DSISTEMA_KINSA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SISTEMA_KINSASISTEMA_KINSA  5   	       SISTEMA_KINSA.FormaPago   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:SISTEMA_KINSA     5   	     � Title$  	FormaPago��5   	   $	��450��6   $	��800��6(   .��5	      � ��5
   
   
� 5      .��5      �  � Color$ 	#FF8A3287��6    � Offset$ 1Q�61   � 5      $ 	#FF67BCCC��6   $ 0Q�61   5      .+�5   
   �� $$��INGRESO DE PAGO A RECOLECTORES��6   $��25(�69   $��Center=�6G   $��0,35,0,0q�6d   $	��Top=�6v   $
��Bold"�6�   �� 5   
   $��HORAS TRABAJADAS��6   $��15(�6+   $
��Left=�69   $��
67,127,0,0q�6T   $	��Top=�6h   �� 5   
   -   5         ��Name$ txtHorasTrabajadas��5      $
��Left=�6.   $��20(�6I   $��
79,157,0,0q�6W    �� TextWrapping$
 Wrap=�6k   $	��Top=�6�   $	��126��6�   �� 5   
   $��HORAS EXTRAS��6   $��15(�6'   $
��Left=�65   $��265,127,0,0q�6P   $	��Top=�6e   �� 5   
   -   5        $ txtHorasExtras��5      $
��Left=�6*   $��20(�6E   $��259,157,0,0q�6S   $
 Wrap=�6h   $	��Top=�6}   $	��126��6�   �� 5   
   $��PAGO POR HORA��6   $��15(�6(   $
��Left=�66   $��427,127,0,0q�6Q   $	��Top=�6f   �� 5   
   -   5        $ txtPagoHora��5      $
��Left=�6'   $��20(�6B   $��427,157,0,0q�6P   $
 Wrap=�6e   $	��Top=�6z   $	��126��6�   �� 5   
   $��FORMA DE PAGO��6   $��15(�6(   $
��Left=�66   $��597,127,0,0q�6Q   $	��Top=�6f   �� 5   
   -   5        $ txtFormaPago��5      $
��Left=�6(   $��20(�6C   $��596,157,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6{   $	��126��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtpckFechaPago��5      $
��Left=�6-   $��
70,257,0,0q�6H   $	��Top=�6\   �� 5   
   $��FECHA DE PAGO��6   $��15(�6(   $
��Left=�66   $��
83,222,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5         ��Name$ btnGenerarPago��5      $��GENERAR PAGO��6)   $
��Left=�6@   $��570,254,0,0q�6[   $	��Top=�6p   $
��Bold"�6�   $��27��6�   $	��110��6�   �� 5   
   -   5         ��Name$ cmbFormaPago��5      $
��Left=�6)   $��265,253,0,0q�6D   $	��Top=�6Y   $	��152��6q   $��28��6}   �� 5   
   $��DETALLES��6   $��15(�6#   $
��Left=�61   $��304,216,0,0q�6L   $	��Top=�6a   5      5       