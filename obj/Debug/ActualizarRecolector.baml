   M S B A M L   `   `   `  ����H  DSISTEMA_KINSA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SISTEMA_KINSASISTEMA_KINSA  5   	   (    "SISTEMA_KINSA.ActualizarRecolector   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:SISTEMA_KINSA     5   	     � Title$  ActualizarRecolector��5   	   $	��720��6&   $
��1355��63   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF52D8CF��6   � 5      $ 	#FF3A8FB3��6    � Offset$ 1Q�61   5      .+�5   
   �� $��ACTUALIZAR RECOLECTOR��6   $��25(�60   $
��Left=�6>   $��
502,15,0,0q�6Y   $	��Top=�6m   $��48��6�   $	��373��6�   ���	#FF04070E$
��Bold"�6�   �� 5   
   $T��NNOTA: Ingrese en todos los campos la información del recolector correctamente��6   $��15(�6h   $
��Left=�6v   $��
426,63,0,0q�6�   $	��Top=�6�   �� 5   
   $��PRIMER NOMBRE��6   $��15(�6(   $
��Left=�66   $��131,121,0,0q�6Q   $	��Top=�6f   �� 5   
   -   5         ��Name$ txtPrimerNombre��5      $
��Left=�6+   $��20(�6F   $��135,151,0,0q�6T    �� TextWrapping$
 Wrap=�6i   $	��Top=�6}   $	��120��6�   �� 5   
   $��SEGUNDO NOMBRE��6   $��15(�6)   $
��Left=�67   $��342,121,0,0q�6R   $	��Top=�6g   �� 5   
   -   5        $ txtSegundoNombre��5      $
��Left=�6,   $��20(�6G   $��355,151,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $	��120��6�   �� 5   
   $��PRIMER APELLIDO��6   $��15(�6*   $
��Left=�68   $��587,121,0,0q�6S   $	��Top=�6h   �� 5   
   -   5        $ txtPrimerApellido��5      $
��Left=�6-   $��20(�6H   $��593,151,0,0q�6V   $
 Wrap=�6k   $	��Top=�6   $	��120��6�   �� 5   
   $��SEGUNDO APELLIDO��6   $��15(�6+   $
��Left=�69   $��805,121,0,0q�6T   $	��Top=�6i   �� 5   
   -   5        $ txtSegundoApellido��5      $
��Left=�6.   $��20(�6I   $��819,151,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $	��120��6�   �� 5   
   $��NUMERO DE CÉDULA��6   $��15(�6+   $
��Left=�69   $��1062,121,0,0q�6T   $	��Top=�6j   �� 5   
   -   5        $ txtNumeroCedula��5      $
��Left=�6+   $��20(�6F   $��1074,151,0,0q�6T   $
 Wrap=�6j   $	��Top=�6~   $	��120��6�   �� 5   
   $��NUMERO DE CELULAR��6   $��15(�6,   $
��Left=�6:   $��115,199,0,0q�6U   $	��Top=�6j   �� 5   
   -   5        $ txtNumeroCelularReco��5      $
��Left=�60   $��20(�6K   $��135,229,0,0q�6Y   $
 Wrap=�6n   $	��Top=�6�   $	��120��6�   �� 5   
   $��CORREO ELECTRONICO��6   $��15(�6-   $
��Left=�6;   $��335,200,0,0q�6V   $	��Top=�6k   �� 5   
   -   5        $ 	txtCorreo��5      $
��Left=�6%   $��20(�6@   $��328,229,0,0q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��183��6�   �� 5    
   $��TIPO DE SANGRE��6   $��15(�6)   $
��Left=�67   $��595,199,0,0q�6R   $	��Top=�6g   �� 5!   
   -   5        $ txtTipoSangre��5!      $
��Left=�6)   $��20(�6D   $��598,229,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��120��6�   �� 5"   
   $��CARGAS FAMILIARES��6   $��15(�6,   $
��Left=�6:   $��813,199,0,0q�6U   $	��Top=�6j   �� 5#   
   -	   5        $ txtCargasFamiliares��5#      $
��Left=�6/   $��20(�6J   $��827,229,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��120��6�   �� 5$   
   $��DIRECCION DOMICILIARIA ��6   $��15(�62   $
��Left=�6@   $��1053,199,0,0q�6[   $	��Top=�6q   �� 5%   
   -
   5        $ txtDireccion��5%      $
��Left=�6(   $��20(�6C   $��1057,229,0,0q�6Q   $
 Wrap=�6g   $	��Top=�6{   $	��186��6�   �� 5&   
   $��FECHA DE NACIMIENTO��6   $��15(�6.   $
��Left=�6<   $��606,473,0,0q�6W   $	��Top=�6l   (  "System.Windows.Controls.DatePicker  5'   
   -   5          Name$ dtpckFechaNacimiento��5'      $
��Left=�63   $��20(�6N   $��584,501,0,0q�6\   $	��Top=�6q   $��40��6�   $	��230��6�   �� 5(   
   $$��NOMBRE DE REFERENCIA
 FAMILIAR��6   $��15(�6=   $
��Left=�6K   $��104,281,0,0q�6f   $	��Top=�6{   �� 5)   
   -   5        $ txtNombreReferencia��5)      $
��Left=�6/   $��20(�6J   $��134,333,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��120��6�   �� 5*   
   $%��CELULAR DE REFERENCIA
 FAMILIAR��6   $��15(�6>   $
��Left=�6L   $��330,281,0,0q�6g   $	��Top=�6|   �� 5+   
   -   5        $ txtCelularReferencia��5+      $
��Left=�60   $��20(�6K   $��359,333,0,0q�6Y   $
 Wrap=�6n   $	��Top=�6�   $	��120��6�   �� 5,   
   $��SALARIO��6   $��15(�6"   $
��Left=�60   $��621,301,0,0q�6K   $	��Top=�6`   �� 5-   
   -   5        $ 
txtSalario��5-      $
��Left=�6&   $��20(�6A   $��594,333,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��120��6�   �� 5.   
   $��FECHA DE INGRESO��6   $��15(�6+   $
��Left=�69   $��872,471,0,0q�6T   $	��Top=�6i     5/   
   -   5        $ dtpckFechaIngreso��5/      $
��Left=�60   $��20(�6K   $��842,501,0,0q�6Y   $	��Top=�6n   $��40��6�   $	��230��6�   �� 50   
   $��TIENE ALGUNA ENFERMEDAD��6   $��15(�62   $
��Left=�6@   $��322,471,0,0q�6[   $	��Top=�6p   �� 51   
   $��TIENE ALGUNA DISCAPACIDAD��6   $��15(�64   $
��Left=�6B   $��
78,472,0,0q�6]   $	��Top=�6q   �� 52   
   $��SELECCIONE EL GENERO��6   $��15(�6/   $
��Left=�6=   $��809,299,0,0q�6X   $	��Top=�6m   �� 53   
   -   5         ��Name$ 	cmbGenero��53      $
��Left=�6&   $��20(�6A   $��830,329,0,0q�6O   $	��Top=�6d   $	��120��6|   �� 54   
   $��SELECCIONE EL BARRIO��6   $��15(�6/   $
��Left=�6=   $��106,392,0,0q�6X   $	��Top=�6m   �� 55   
   -   5        $ 	cmbBarrio��55      $
��Left=�6&   $��20(�6A   $��129,422,0,0q�6O   $	��Top=�6d   $	��120��6|   �� 56   
   $��SELECCIONE EL CANTON��6   $��15(�6/   $
��Left=�6=   $��336,392,0,0q�6X   $	��Top=�6m   �� 57   
   -   5        $ 	cmbCanton��57      $
��Left=�6&   $��20(�6A   $��364,422,0,0q�6O   $	��Top=�6d   $	��120��6|   �� 58   
   $��SELECCIONE LA PROVINCIA��6   $��15(�62   $
��Left=�6@   $��570,392,0,0q�6[   $	��Top=�6p   �� 59   
   -   5        $ cmbProvincia��59      $
��Left=�6)   $��20(�6D   $��597,422,0,0q�6R   $	��Top=�6g   $	��120��6   �� 5:   
   $��SELECCIONE EL PAIS��6   $��15(�6-   $
��Left=�6;   $��818,392,0,0q�6V   $	��Top=�6k   �� 5;   
   -   5        $ cmbPais��5;      $
��Left=�6$   $��20(�6?   $��830,422,0,0q�6M   $	��Top=�6b   $	��120��6z   �� 5<   
   $ ��SELECCIONE EL ESTADO CIVIL��6   $��15(�65   $
��Left=�6C   $��1030,299,0,0q�6^   $	��Top=�6t   �� 5=   
   -   5        $ cmbEstadoCivil��5=      $
��Left=�6+   $��20(�6F   $��1074,329,0,0q�6T   $	��Top=�6j   $	��120��6�   �� 5>   
   -   5        	 ��Name$	 btnActualizarRecolector��5>      $��
ACTUALIZAR��62   $��15(�6G   $
��Left=�6U   $��827,620,0,0q�6p   $	��Top=�6�   6�   �� 5?   
   -   5        $ txtDescDiscapacidad��5?      $
��Left=�6/   $��115,579,0,0q�6J   $
 Wrap=�6_   $	��Top=�6s   $	��134��6�   $	��129��6�   �� 5@   
   $!��DESCRIPCION DE DISCAPACIDAD��6   $��15(�66   $
��Left=�6D   $��
66,544,0,0q�6_   $	��Top=�6s   �� 5A   
   -   5        $ txtDescEnfermedad��5A      $
��Left=�6-   $��359,579,0,0q�6H   $
 Wrap=�6]   $	��Top=�6q   $	��134��6�   $	��129��6�   �� 5B   
   $ ��DESCRIPCION DE  ENFERMEDAD��6   $��15(�65   $
��Left=�6C   $��324,544,0,0q�6^   $	��Top=�6s   �� 5C   
   -   5        $ txtDiscapacidad��5C      $
��Left=�6+   $��20(�6F   $��126,507,0,0q�6T   $
 Wrap=�6i   $	��Top=�6}   $	��120��6�   �� 5D   
   -   5        $ txtEnfermedad��5D      $
��Left=�6)   $��20(�6D   $��366,508,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��120��6�   �� 5E   
   $��ESTADO��6   $��15(�6!   $
��Left=�6/   $��1102,396,0,0q�6J   $	��Top=�6`   �� 5F   
   -   5        $ 	txtEstado��5F      $
��Left=�6%   $��20(�6@   $��1074,426,0,0q�6N   $
 Wrap=�6d   $	��Top=�6x   $	��120��6�   5H      5I      