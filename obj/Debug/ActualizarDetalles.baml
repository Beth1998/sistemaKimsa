   M S B A M L   `   `   `  ����H  DSISTEMA_KINSA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SISTEMA_KINSASISTEMA_KINSA  5   	   &     SISTEMA_KINSA.ActualizarDetalles   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:SISTEMA_KINSA     5   	     � Title$  ActualizarDetalles��5   	   $	��450��6$   $	��800��61   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF51D8BC��6   � 5      $ 	#FFA171DC��6    � Offset$ 1Q�61   5      .+�5   
   �� $��INGRESE EL PESO��6   $��15(�6*   $
��Left=�68   $��127,155,0,0q�6S   $	��Top=�6h   �� 5   
   -   5         ��Name$ txtPeso��5      $
��Left=�6#   $��20(�6>   $��127,185,0,0q�6L    �� TextWrapping$
 Wrap=�6a   $	��Top=�6v   $	��126��6�   �� 5   
   -   5         ��Name$ btnActualizarDetalles��5      $��
ACTUALIZAR��60   $��15(�6E   $
��Left=�6S   $��365,354,0,0q�6n   $	��Top=�6�   6�   �� 5   
   $��FECHA DE ENTREGA��6   $��15(�6+   $
��Left=�69   $��307,152,0,0q�6T   $	��Top=�6i   $	��146��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtpckFechaEntrega��5      $
��Left=�60   $��20(�6K   $��286,181,0,0q�6Y   $	��Top=�6n   $��34��6�   $	��222��6�   �� 5   
   $��SELECCIONE EL CLIENTE��6   $��15(�60   $
��Left=�6>   $��521,152,0,0q�6Y   $	��Top=�6n   �� 5   
   -   5        	 ��Name$	 
cmbCliente��5      $
��Left=�6'   $��20(�6B   $��546,182,0,0q�6P   $	��Top=�6e   $	��120��6}   �� 5   
   $��TIPO DE RESIDUO��6   $��15(�6*   $
��Left=�68   $��249,239,0,0q�6S   $	��Top=�6h   �� 5   
   -   5        $	 cmbTipoResiduo��5      $
��Left=�6+   $��20(�6F   $��253,269,0,0q�6T   $	��Top=�6i   $	��120��6�   �� 5   
   $��
RECOLECTOR��6   $��15(�6%   $
��Left=�63   $��451,239,0,0q�6N   $	��Top=�6c   �� 5   
   -   5        $	 cmbRecolector��5      $
��Left=�6*   $��20(�6E   $��440,269,0,0q�6S   $	��Top=�6h   $	��120��6�   �� 5   
   $��ACTUALIZAR DETALLES��6   $��25(�6.   $��Center=�6<   $��0,16,0,0q�6Y   $	��Top=�6k   $
��Bold"�6�   �� 5   
   $E��?NOTA: Ingrese en todos los campos la información correctamente��6   $��15(�6Y   $��Center=�6g   $��0,64,0,0q�6�   $	��Top=�6�   5      5      