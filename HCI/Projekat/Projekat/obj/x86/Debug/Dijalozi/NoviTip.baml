   M S B A M L   `   `   `  ����C  ?Projekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Projekat.DijaloziProjekat.Dijalozi  5   	   9!clr-namespace:Projekat.ValidacijaProjekat.Validacija  5   	   -clr-namespace:Projekat.HelpProjekat.Help  5   	       Projekat.Dijalozi.NoviTip   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Projekat.Dijalozi     5   	   8
validacija!clr-namespace:Projekat.Validacija     5   	   )hclr-namespace:Projekat.Help     5   	      Projekat.Help.HelpProvider    
HelpString$  noviTip��5	   	    � Icon$+ %/Projekat;component/Images/defLoc.png��5   	    � 
ResizeMode$ NoResize=�5   	    � Title$ Novi tip��5   	   $��338.144��6   $��313.625��6+   $��14(�6;   .��5      � $	��307��6   $	��Top=�6   .+�5   
   �� -   5         ��Name$ label��5      $��Oznaka:��6   $
��Left=�61   $��27��6L   $��	10,22,0,0q�6X   $	��Top=�6k   $��67��6�   �� 5   
   -   5        $ 
label_Copy��5      $��Naziv:��6$   $
��Left=�65   $��27��6P   $��	10,54,0,0q�6\   $	��Top=�6o   $��67��6�   �� 5   
   -   5        $ label_Copy1��5      $��Opis:��6%   $
��Left=�65   $��27��6P   $��	10,86,0,0q�6\   $	��Top=�6o   $��67��6�   �� 5   
   -   5        $ label_Copy2��5      $��Ikonica:��6%   $
��Left=�68   $��27��6S   $��
10,158,0,0q�6_   $	��Top=�6s   $��67��6�   �� 5   
   -   5         ��Name$ oznaka_textBox��5      $  noviTip��6*   $
��Left=�6O   $��27��6j   $��	77,22,0,0q�6v    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��205��6�   $��14(�6�   .��6�    � ToolTip$L FObavezan unos oznake tipa. Dozvoljen unos slova, brojeva i donje crte.��5      .����5      �� 5       �� Path$ Oznaka�6   	 �� UpdateSourceTrigger$	 PropertyChanged=�6(   
 �� ValidationRules
 5      *  $Projekat.Validacija.OznakaValidation  5      ,  &System.Windows.Controls.ValidationRule   ValidatesOnTargetUpdated . 66   5      .��5       =� ErrorTemplate 5      �� 5       .R�5!      �  � RowDefinitions 5"      �� 5#       � ColumnDefinitions 5%      �� 5&      �� 5'      $
��Auto �6/   .+�5*      ��  ��Name$ textBox��6Q   $��0��64   $��0��6D   6Q   �� 5+      $��1��6$   $��0��64   
���  ��$��Center=�6t   $��22(�6�   $��Medium"�6�   ��6A   �� *[0].ErrorContent+6�   5,      5-      .��5.   (   5/      �� 50   
   -   5        $ naziv_textBox��50      $  noviTip��6)   $
��Left=�6N   $��27��6i   $��	77,54,0,0q�6u   $
 Wrap=�6�   $	��Top=�6�   $	��205��6�   $��14(�6�   .��6�   $L FObavezan unos naziva tipa. Dozvoljen unos slova, brojeva i donje crte.��53      .����54      �� 55      $ Naziv�6   $	 PropertyChanged=�6#   
 56        57       . 62   59      .��5:       5;      �� 5<      .R�5=      �  5>      �� 5?       5A      �� 5B      �� 5C      $
��Auto �6/   .+�5F      �� $ textBox��6Q   $��0��64   $��0��6D   6Q   �� 5G      $��1��6$   $��0��64   
���  ��$��Center=�6t   $��22(�6�   $��Medium"�6�   ��6A   �� *[0].ErrorContent+6�   5H      5I      .��5J   (   5K      �� 5L   
   -   5        $ opis_textBox��5L      $  noviTip��6(   $ Opcion unos opisa tipa��6M   $
��Left=�6�   $��67��6�   $��	77,86,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��205��6�   $��14(�6�   ��6n   �� *Opis+6�   �� 5M   
   -   5         ��Name$ ikonica��5M      $ Opcion unos slike tipa��6!   $
��Left=�6B   $��70��6]   $��
77,158,0,0q�6i   $	��Top=�6}   $��75��6�   �� 5N   
   
���   �$��1q�6%   $
��Left=�69   $��70��6T   $��
77,158,0,0q�6`   $	��Top=�6t   $��75��6�   �� 5O   
   -	   5         ��Name$ izaberiIkonicu��5O      $ Opcion unos slike tipa��6)   $��Izaberi ikonicu��6J   $
��Left=�6d   $��27��6   $��168,179,0,0q�6�   $	��Top=�6�   $	��101��6�   6�   �� 5P   
   -
   5        $ sacuvaj��5P      $��Sačuvaj��6"   $��62,270,0,10q�64   $��27��6I   $
��Left=�6U   $��90��6p   6{   �� 5Q   
   -   5        $ odustani��5Q      $��Odustani��6#   $��168,270,0,10q�67   $��27��6M   $
��Left=�6Y   $��87��6t   6   5S      5T      