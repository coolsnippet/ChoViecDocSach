<!-- TOC insertAnchor:true orderedList:true -->

1. [Look for an element](#look-for-an-element)
    1. [1. select/find a element/node contains value](#1-selectfind-a-elementnode-contains-value)
    2. [2. Find an element whose ancestor has some condition:](#2-find-an-element-whose-ancestor-has-some-condition)
2. [Modify value of an element](#modify-value-of-an-element)
3. [Get element detail](#get-element-detail)
    3. [by Attributes index](#by-attributes-index)
    4. [by GetAttributeValue function](#by-getattributevalue-function)

<!-- /TOC -->

<a id="markdown-look-for-an-element" name="look-for-an-element"></a>
# Look for an element

<a id="markdown-1-selectfind-a-elementnode-contains-value" name="1-selectfind-a-elementnode-contains-value"></a>
## 1. select/find a element/node contains value
from http://stackoverflow.com/questions/3844208/html-agility-pack-find-comment-node
e.g I want to look for a comment
htmlDoc.DocumentNode.SelectSingleNode("//comment()[contains(., 'Buying Options')]/following-sibling::script")

<a id="markdown-2-find-an-element-whose-ancestor-has-some-condition" name="2-find-an-element-whose-ancestor-has-some-condition"></a>
## 2. Find an element whose ancestor has some condition:
//*[@class='nw_boxing_title' and ancestor::*[@class='nw_book_tree']]

<a id="markdown-modify-value-of-an-element" name="modify-value-of-an-element"></a>
# Modify value of an element

<a id="markdown-get-element-detail" name="get-element-detail"></a>
# Get element detail

<a id="markdown-by-attributes-index" name="by-attributes-index"></a>
## by Attributes index
item.Attributes["href"].Value // item is the htmlnode

<a id="markdown-by-getattributevalue-function" name="by-getattributevalue-function"></a>
## by GetAttributeValue function
n.GetAttributeValue("class", "").Equals("pd_description nw_zoomcontent normal"))

