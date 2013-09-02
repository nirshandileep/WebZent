function numbersOnly(mField, e, dec)
{			
	var key;
	var keychar;

	if (window.event)
	{
		key = window.event.keyCode;
	}
	else if (e)
	{
		key = e.which;
	}
	else
	{
		return true;
	}

	keychar = String.fromCharCode(key);			

	// control keys
	if ((key == null) || (key ==  0) || (key ==  8) ||
			(key == 9)    || (key == 13) || (key == 27) )
	{
		return true;

	// numbers
	}
	else if ((("0123456789").indexOf(keychar) > -1))
	{
		return true;
	}
	// decimal point jump
	else if (dec && (keychar == '.') && mField.value.indexOf(".") == -1)
	{				
		//mField.form.elements[dec].focus();
		return true;
	}
	else
	{
		return false;
	}
}

function isNumberKey(evt)
{

     var charCode = (evt.which) ? evt.which : event.keyCode;
     if (charCode > 31 && (charCode < 48 || charCode > 57) )
        return false;

     return true;
}

function isDecimalKey(evt)
{
//alert('function called');
     var charCode = (evt.which) ? evt.which : event.keyCode;
     if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode!=46)
        return false;

     return true;
}

function isSlash(evt)
{
//alert('function called');
     var charCode = (evt.which) ? evt.which : event.keyCode;
     if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode!=47)
        return false;

     return true;
}

function ClientValidate(source, arguments) 
{ 
    var txtUserName=document.getElementById("txtUserName");
    var txtPassword=document.getElementById("txtPassword");
    if ((txtUserName.value == "" ) || (txtPassword.value =="")) 
    { 
        arguments.IsValid = false; 
        //alert("ValidateDualFields fired."); 
    }	
    
}

function ClientValidateTelephoneNumber(source, arguments) 
{ 
    var txtPart1=document.getElementById("ctl00_ContentPlaceHolder1_txtAscPhone1");
    var txtPart2=document.getElementById("ctl00_ContentPlaceHolder1_txtAscPhone2");
    var txtPart3=document.getElementById("ctl00_ContentPlaceHolder1_txtAscPhone3");
    
    if ((txtPart1.value == "" ) || (txtPart2.value =="") || (txtPart3.value == "" ) || (((txtPart1.value.length)+(txtPart2.value.length)+(txtPart3.value.length))<10)) 
    { 
        arguments.IsValid = false; 
        //alert("ValidateDualFields fired.");
    }	
     
}

function imposeMaxLength(Object, MaxLen)
{
  return (Object.value.length <= MaxLen);
}

function noenter() 
{
  return !(window.event && window.event.keyCode == 13); 
}

function noSpaceBar(evt)
{ 
     var charCode = (evt.which) ? evt.which : event.keyCode;
     if (charCode == 32)
        return false;

     return true;
}

function isHidden1to8Empty(s,a)
{
    var hdn1to8 =document.getElementById("ctl00_ContentPlaceHolder1_hdn1to8");
    //alert(hdn1to8.value);
    var hdn9to16=document.getElementById("ctl00_ContentPlaceHolder1_hdn9to16");
    //alert(hdn9to16.value);
    var hdn17to24=document.getElementById("ctl00_ContentPlaceHolder1_hdn17to24");
    //alert(hdn17to24.value);
    if ((hdn1to8.value == "" ) || (hdn9to16.value =="") || (hdn17to24.value == "" )) 
    { 
        a.IsValid = false; 
       // alert("ValidateDualFields fired."); 
    }
}


//function SelectAll(id)
//        {
//        //alert(id); 
//            //get reference of GridView control
//            var grid = document.getElementById('ctl00_ContentPlaceHolder1_gvSurveyList');
//            //variable to contain the cell of the grid
//            var cell;
//            
//            if (grid.rows.length > 0)
//            {
//                //loop starts from 1. rows[0] points to the header.
//                for (i=1; i<grid.rows.length; i++)
//                {
//                    //get the reference of first column
//                    cell = grid.rows[i].cells[6];
//                    
//                    //loop according to the number of childNodes in the cell
//                    for (j=0; j<cell.childNodes.length; j++)
//                    {   //alert(cell.childNodes.length);        
//                        //if childNode type is CheckBox   
//                        if (cell.childNodes[j].type =="checkbox")
//                        {
//                           // alert(cell.childNodes[j].type); 
//                            //assign the status of the Select All checkbox to the cell checkbox within the grid
//                            cell.childNodes[j].checked = document.getElementById(id).checked;
//                        }
//                    }
//                }
//            }
//        }
//   
//    function UnCheckSelectAll(id)
//        {
//            
//            //ctl00_ContentPlaceHolder2_gvAvailableCourseList_ctl01_chkSelectAll
//          
//                             
//                     //alert(id);                
//                        if (document.getElementById(id).checked)
//                        {
//                        
//                        }else
//                        {
//                        var chkbx =  document.getElementById('ctl00_ContentPlaceHolder1_gvSurveyList_ctl01_chkSelectAll');
//                        chkbx.checked = false;
//                        }
//        }