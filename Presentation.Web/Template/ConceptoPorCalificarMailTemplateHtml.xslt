<?xml version='1.0'?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:formatHelper="urn:xsltExtension-FormatHelper" >

	<xsl:output method="html"/>

	<!-- Get config.file -->


	<xsl:template match="/">

		<html>
			<head>
			</head>
			<body>
       
				<table style="BACKGROUND-COLOR: #317EAC;" width="100%" cellpadding="2px">
					<tr>
						<td>              
							<table style="FONT-FAMILY:Arial; FONT-SIZE:12px; BACKGROUND-COLOR:#FFFFFF; COLOR:Black" align="center" width="100%" cellpadding="2px">
                <tr>
                  <td colspan="2">
                    <p>
                      Hola, <xsl:value-of select="//@name"/>: <br/>
                      Se registrado un concepto a un proyecto de ley que usted esta relacionado. Para calificar el concepto hacer click <a href="{//@url_calificar_concept}">aqui</a>
                    </p>
                  </td>
                </tr>
								
							</table>
						</td>
					</tr>
				</table>
			</body>
		</html>

	</xsl:template>

</xsl:stylesheet>